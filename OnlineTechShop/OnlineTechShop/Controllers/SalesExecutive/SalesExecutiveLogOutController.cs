using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveLogOutController : Controller
    {
        public ActionResult Index()
        {
            Session.Clear();
            return RedirectToAction("Index", "SaLesExecutiveLogin");
        }
    }
}