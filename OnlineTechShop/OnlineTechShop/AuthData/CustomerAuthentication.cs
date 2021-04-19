using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace OnlineTechShop.AuthData
{
    public class CustomerAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user_email"] == null)
            {
                filterContext.Controller.TempData["msg"] = "Please login first!";
                filterContext.Result = new RedirectResult("/CustomerLogin");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}