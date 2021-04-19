using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class LogoutController : AdminBaseController
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session.Clear();
            Session["email"] = "";
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}