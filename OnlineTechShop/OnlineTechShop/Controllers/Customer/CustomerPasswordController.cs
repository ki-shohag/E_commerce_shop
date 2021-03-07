using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerPasswordController : Controller
    {
        // GET: CustomerPassword
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VerifyEmail()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
    }
}