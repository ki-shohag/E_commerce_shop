using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerRegistrationController : Controller
    {
        // GET: CustomerRegistration
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}