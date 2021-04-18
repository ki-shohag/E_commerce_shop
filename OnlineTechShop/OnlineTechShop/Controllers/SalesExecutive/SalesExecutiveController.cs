using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using OnlineTechShop.Models.Sales.ViewModels;
using OnlineTechShop.Models.Sales.DataModels;
using System.Web.Script.Serialization;



namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveController : Controller
    {
        TechShopDbEntities context = new TechShopDbEntities();
        public int seid2 = 0;

        CartDataModel cartData = new CartDataModel();
        ProductsDataModel productsData = new ProductsDataModel();

        // GET: SalesExecutive
        public ActionResult Index()
        {
            var seid = Session["seid"];

            if (Session["seid"] == null)
            {
                TempData["msg"] = "Please First Login";
                return RedirectToAction("Index", "SalesExecutiveLogin");
            }
            else {
                return View(context.SalesExecutives.Find(seid));
            }


        }
        public ActionResult Products()
        {
            if (Session["seid"] == null)
            {
                return RedirectToAction("Index", "SalesExecutiveLogin");
            }
            else
            {
                return View(context.Products.ToList());
            }
        }
        public ActionResult AvailableProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return View(data.GetAvailableProducts());
        }
        public ActionResult UpcomingProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return View(data.GetUpComingProducts());
        }

        public ActionResult DiscountProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return View(data.GetAllDiscountProducts());
        }
        [HttpGet]
        public ActionResult SearchByCategory(string SearchValue)
        {
            ProductsDataModel data = new ProductsDataModel();

             //return Json(data.GetProductByCategory(SearchValue, 5), JsonRequestBehavior.AllowGet);
             return Json(JsonConvert.SerializeObject(data.GetProductByCategory(SearchValue, 5)), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SearchByName(string SearchValue)
        {
            ProductsDataModel data = new ProductsDataModel();

            //return Json(data.GetProductByCategory(SearchValue, 5), JsonRequestBehavior.AllowGet);
            return Json(JsonConvert.SerializeObject(data.GetProductByName(SearchValue)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesCart()
        {
            Object obj = Session["SalesCart"];
            if (obj == null)
            {
                TempData["msg"] = "Cart is empty!";
                List<CartViewModel> carts = null;
                if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
                return View(carts);
            }
            else
            {
                List<CartViewModel> carts = (List<CartViewModel>)obj;
                if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
                return View(carts);
            }
        }

        [HttpPost]
        public ActionResult AddProductToCart(CartViewModel data)
        {
            CartViewModel cart = cartData.ConvertToCartViewModel(productsData.GetProductById(data.ProductId));
            int productStockQuantity = productsData.GetProductStockQuantity(data.ProductId);
            int totalCartQuantity = 0;
            if (productStockQuantity >= data.Quantity)
            {
                cart.Quantity = data.Quantity;
                if (cart.Quantity == 0) { return Json(JsonConvert.SerializeObject(new { msg = "Cant Add 0 Quantity!"}), JsonRequestBehavior.AllowGet); }
                if (Session["SalesCart"] == null)
                {
                    var currentCart = cartData.GetNewCart();
                    currentCart.Add(cart);
                    totalCartQuantity = cart.Quantity;
                    Session["SalesCart"] = currentCart;
                    Session["cart_quantity"] = totalCartQuantity;
                }
                else
                {
                    var currentCart = (List<CartViewModel>)Session["SalesCart"];
                    var addedCart = currentCart.Where(x => x.ProductId == data.ProductId).FirstOrDefault();
                    currentCart.Remove(addedCart);
                    if (addedCart != null)
                    {
                        addedCart.Quantity += data.Quantity;
                        if (addedCart.Quantity > productStockQuantity)
                        {
                            addedCart.Quantity -= data.Quantity;
                            currentCart.Add(addedCart);
                            totalCartQuantity = (int)Session["cart_quantity"];
                            return Json(JsonConvert.SerializeObject(new { msg = "Not much stock available! Only " + productStockQuantity + " pcs left!", totalCartQuantity = totalCartQuantity }), JsonRequestBehavior.AllowGet);
                        }
                        currentCart.Add(addedCart);
                    }
                    else
                    {
                        currentCart.Add(cart);
                    }
                    totalCartQuantity = (int)currentCart.Sum(x => x.Quantity);
                    Session["SalesCart"] = currentCart;
                    Session["cart_quantity"] = totalCartQuantity;
                }
                return Json(JsonConvert.SerializeObject(new { msg = "Added prodcut!", totalCartQuantity = totalCartQuantity }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { msg = "Not much stock left!", totalCartQuantity = totalCartQuantity }), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult RemoveProductFromCart(int id)
        {
            if (Session["SalesCart"] == null)
            {
                if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
                return RedirectToAction("SalesCart");
            }
            else
            {
                var currentCart = (List<CartViewModel>)Session["SalesCart"];
                var deletingCart = currentCart.Where(x => x.ProductId == id).FirstOrDefault();
                currentCart.Remove(deletingCart);
                //int cartQuantity = (int)Session["cart_quantity"];
                //cartQuantity -= deletingCart.Quantity;
                //Session["cart_quantity"] = cartQuantity;
                Session["SalesCart"] = currentCart;
                TempData["msg"] = "Proudct is removed!";
                if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
                return RedirectToAction("SalesCart");
            }
        }
        [HttpGet]
        public ActionResult ClearCart()
        {
                Session["SalesCart"] = null;
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return RedirectToAction("SalesCart");
            
            
          
        }
        [HttpGet]
        public ActionResult Sell()
        {
            Object obj = Session["SalesCart"];

            List<CartViewModel> carts = (List<CartViewModel>)obj;
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return View(carts);

        }
        [HttpPost]
        public ActionResult Sell(FormCollection formCollection )
        {
            Object obj = Session["SalesCart"];
            seid2 = (int)Session["seid"];

            if (formCollection.Get("FullName") =="" ) { TempData["fn"] = "The Name field is required"; return RedirectToAction("Sell"); }
            else if (formCollection.Get("Address") == "") { TempData["a"] = "The Address field is required"; return RedirectToAction("Sell"); }
            else if (formCollection.Get("Phone") == "") { TempData["p"] = "The Phone field is required"; return RedirectToAction("Sell"); }
            List<CartViewModel> carts = (List<CartViewModel>)obj;

            ProductsDataModel data = new ProductsDataModel();
            Models.Product SellingProduct = new Product();
          
          
            var CurrentQuantity=0;
            var UpdatedQuantity=0;
            var id=0;
            foreach ( var c in carts) {
               
                id = c.ProductId;
    
                SellingProduct = data.GetProductById(id);

                CurrentQuantity = SellingProduct.Quantity;
                UpdatedQuantity = CurrentQuantity - c.Quantity;
                if (UpdatedQuantity == 0) { SellingProduct.Status = "Stock Out"; }
                SellingProduct.Quantity = UpdatedQuantity;
                data.UpdateProduct(SellingProduct);
                var sl = new Sales_Log() {
                    UserId = 2,
                    ProductId=c.ProductId,
                    SalesExecutiveId= seid2,
                    DateSold= DateTime.Today,
                    Quantity = c.Quantity,
                    Discount= SellingProduct.Discount,
                    Tax= SellingProduct.Tax,
                    TotalPrice=(decimal)c.TotalPrice,
                    Status="Sold",
                    Profits= ((SellingProduct.SellingPrice) *c.Quantity) - ((SellingProduct.BuyingPrice) * c.Quantity)
                };
                context.Sales_Log.Add(sl);
                context.SaveChanges();
            }
            // SellingProduct = data.GetProductById(id);

            // SellingProduct.Quantity = SellingProduct.Quantity -1;

            //context.Entry(SellingProduct).State = System.Data.Entity.EntityState.Modified;
            //context.SaveChanges();
            // data.UpdateProduct(SellingProduct);

            Session["SalesCart"] = null;
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return RedirectToAction("Products");

        }
        public ActionResult SellChart()
        {
            if (Session["seid"] == null) { return RedirectToAction("Index", "SalesExecutiveLogin"); }
            return View();
        }

        public ActionResult LoadChart()
        {
            SalesLogDataModel log = new SalesLogDataModel();
           
           // var categoryNameQuantity = new List<KeyValuePair<string, int>>();
            var DateAndQuantity = new List<KeyValuePair<string, int>>();
            var alllog = log.GetAllSalesLog().ToList();

            Dictionary<DateTime, bool> check = new Dictionary<DateTime, bool>();
            foreach (var item in alllog)
            {
                check.Add((item.DateSold), true);
            }
            foreach (KeyValuePair<DateTime, bool> item in check)
            {
                DateTime currDate = item.Key;
                
                int totalQuantity = context.Sales_Log.Where(x => x.DateSold== currDate).Count();
                DateAndQuantity.Add(new KeyValuePair<string, int>(currDate.ToString(), totalQuantity));
            }
            return Json(new JavaScriptSerializer().Serialize(DateAndQuantity), JsonRequestBehavior.AllowGet);
        }

    }
}