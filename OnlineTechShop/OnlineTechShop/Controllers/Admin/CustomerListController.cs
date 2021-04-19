using OnlineTechShop.Models;
using OnlineTechShop.Models.AdminModel.AdminDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class CustomerListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        CustomerDataModel data = new CustomerDataModel();

        public ActionResult Index()
        {
            List<Models.Customer> customers = data.GetActiveCustomers();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Block(int id)
        {
            var removeCustomer = context.Customers.Find(id);
            return View(removeCustomer);
        }

        [HttpPost]
        public ActionResult BlockCustomer(int id)
        {
            data.BlockCustomer(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Restricted()
        {
            List<Models.Customer> customers = data.GetRestrictedCustomers();
            return View(customers);
        }

        [HttpGet]
        public ActionResult ViewHistory(int id)
        {
            var sales = context.Sales_Log.Where(m=>m.UserId == id);
            return View(sales);
        }

        [HttpGet]
        public ActionResult Reactivate(int id)
        {
            var reactivateCustomer = context.Customers.Find(id);
            return View(reactivateCustomer);
        }

        [HttpPost]
        public ActionResult ReactivateCustomer(int id)
        {
            data.ReactivateCustomer(id);
            return RedirectToAction("Index");
        }

        public ActionResult CustomerReview(int id)
        {
            var givenReviews = context.Reviews.Where(p => p.UserId == id).ToList();
            return View(givenReviews);
        }
    }
}