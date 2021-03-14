using Newtonsoft.Json;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using OnlineTechShop.Models.CustomerAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class CartController : Controller
    {
        CartDataModel cartData = new CartDataModel();
        ProductsDataModel productsData = new ProductsDataModel();
        // GET: Cart
        public ActionResult Index()
        {
            Object obj = Session["cart"];
            if (obj==null)
            {
                TempData["msg"]="Cart is empty!";
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
            if(productStockQuantity > data.Quantity)
            {
                cart.Quantity = data.Quantity;
                if (Session["cart"]==null)
                {
                    var currentCart = cartData.GetNewCart();
                    currentCart.Add(cart);
                    totalCartQuantity = cart.Quantity;
                    Session["cart"] = currentCart;
                    Session["cart_quantity"] = totalCartQuantity;
                }
                else
                {
                    var currentCart = (List<CartViewModel>)Session["cart"];
                    var addedCart = currentCart.Where(x => x.ProductId == data.ProductId).FirstOrDefault();
                    currentCart.Remove(addedCart);
                    if (addedCart!=null)
                    {
                        addedCart.Quantity += data.Quantity;
                        if (addedCart.Quantity > productStockQuantity)
                        {
                            addedCart.Quantity -= data.Quantity;
                            currentCart.Add(addedCart);
                            totalCartQuantity = (int)Session["cart_quantity"];
                            return Json(JsonConvert.SerializeObject(new { msg = "Not much stock available! Only "+productStockQuantity+" pcs left!", totalCartQuantity = totalCartQuantity }), JsonRequestBehavior.AllowGet);
                        }
                        currentCart.Add(addedCart);
                    }
                    else
                    {
                        currentCart.Add(cart);
                    }
                    totalCartQuantity = (int)currentCart.Sum(x => x.Quantity);
                    Session["cart"] = currentCart;
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
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var currentCart = (List<CartViewModel>)Session["cart"];
                var deletingCart = currentCart.Where(x => x.ProductId == id).FirstOrDefault();
                currentCart.Remove(deletingCart);
                int cartQuantity = (int)Session["cart_quantity"];
                cartQuantity -= deletingCart.Quantity;
                Session["cart_quantity"] = cartQuantity;
                Session["cart"] = currentCart;
                TempData["msg"]= "Proudct is removed!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateSelectedProductQuantity(CartViewModel data)
        {
            int stock = productsData.GetProductStockQuantity(data.ProductId);
            int cartQuantity = (int)Session["cart_quantity"];
            if (stock >= data.Quantity)
            {
                var currentCart = (List<CartViewModel>)Session["cart"];
                var selectedCart = currentCart.Where(x => x.ProductId == data.ProductId).FirstOrDefault();
                selectedCart.Quantity = data.Quantity;
                var newCart = selectedCart;
                currentCart.Remove(selectedCart);
                currentCart.Add(newCart);
                int cartQuantityInCurrentCart = (int)currentCart.Sum(x => x.Quantity);
                cartQuantity = cartQuantityInCurrentCart;
                Session["cart_quantity"] = cartQuantityInCurrentCart;
                Session["cart"] = currentCart;
                return Json(JsonConvert.SerializeObject(new { msg = "Updated the cart!", cartQuantity = cartQuantity }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { msg = "Not much stock left! Only "+stock +" pcs left!", cartQuantity = cartQuantity }), JsonRequestBehavior.AllowGet);
            }
        }
    }
}