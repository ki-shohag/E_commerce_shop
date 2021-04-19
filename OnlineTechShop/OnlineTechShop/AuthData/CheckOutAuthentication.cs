using OnlineTechShop.Models.CustomerAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace OnlineTechShop.AuthData
{
    public class CheckOutAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List <CartViewModel> cart = (List<CartViewModel>)filterContext.HttpContext.Session["cart"];
            if (cart==null || cart.Count==0)
            {
                filterContext.Controller.TempData["msg"] = "Your cart is empty!!";
                filterContext.Result = new RedirectResult("/Cart");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}