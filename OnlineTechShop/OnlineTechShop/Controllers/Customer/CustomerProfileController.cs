using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.ViewModels;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerProfileController : Controller
    {
        // GET: CustomerProfile
        [HttpGet]
        public ActionResult Index()
        {
            CustomerViewModel model = new CustomerViewModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult PurchaseHistory(int? id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult WishList(int? id)
        {
            return View();
        }
    }
}