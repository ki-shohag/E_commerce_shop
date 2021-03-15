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


namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveController : Controller
    {
        private TechShopDbEntities context = new TechShopDbEntities();
        CartDataModel cartData = new CartDataModel();
        ProductsDataModel productsData = new ProductsDataModel();

        // GET: SalesExecutive
        public ActionResult Index()
        {
           
            return View(context.SalesExecutives.ToList());
        }
        public ActionResult Products()
        {
           
            return View(context.Products.ToList());
        }
        public ActionResult AvailableProducts()
        {
             ProductsDataModel data = new ProductsDataModel();
            return View(data.GetAvailableProducts());
        }
        public ActionResult UpcomingProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            return View(data.GetUpComingProducts());
        }

        public ActionResult DiscountProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            return View(data.GetAllDiscountProducts());
        }
        public JsonResult SearchByCategory(string SearchValue)
        {
            ProductsDataModel data = new ProductsDataModel();
            return Json(data.GetProductByCategory(SearchValue, 5), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesCart()
        {
            Object obj = Session["SalesCart"];
            if (obj == null)
            {
                TempData["msg"] = "Cart is empty!";
                List<CartViewModel> carts = null;
                return View(carts);
            }
            else
            {
                List<CartViewModel> carts = (List<CartViewModel>)obj;
                return View(carts);
            }
        }

        [HttpPost]
        public ActionResult AddProductToCart(CartViewModel data)
        {
            CartViewModel cart = cartData.ConvertToCartViewModel(productsData.GetProductById(data.ProductId));
            int productStockQuantity = productsData.GetProductStockQuantity(data.ProductId);
            int totalCartQuantity = 0;
            if (productStockQuantity > data.Quantity)
            {
                cart.Quantity = data.Quantity;
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
                return RedirectToAction("SalesCart");
            }
        }
        [HttpGet]
        public ActionResult ClearCart()
        {
                Session["SalesCart"] = null;
            
                return RedirectToAction("SalesCart");
            
            
          
        }
        [HttpGet]
        public ActionResult Sell()
        {
            Object obj = Session["SalesCart"];

            List<CartViewModel> carts = (List<CartViewModel>)obj;
            return View(carts);

        }
        [HttpPost]
        public ActionResult Sell(FormCollection formCollection )
        {
            Object obj = Session["SalesCart"];
            

            if (formCollection.Get("FullName") =="" ) { TempData["fn"] = "The Name field is required"; return RedirectToAction("Sell"); }
            else if (formCollection.Get("Address") == "") { TempData["a"] = "The Address field is required"; return RedirectToAction("Sell"); }
            else if (formCollection.Get("Phone") == "") { TempData["p"] = "The Phone field is required"; return RedirectToAction("Sell"); }
            List<CartViewModel> carts = (List<CartViewModel>)obj;

            return RedirectToAction("Index");

        }

    }
}