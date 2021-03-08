using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerLogOutController : Controller
    {
        // GET: CustomerLogOut
        public ActionResult Index()
        {
            Session.Clear();
            return RedirectToAction("Index","CustomerLogin");
        }
    }
}