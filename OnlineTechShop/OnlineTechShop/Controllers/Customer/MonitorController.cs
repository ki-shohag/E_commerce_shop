using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class MonitorController : Controller
    {
        ProductsDataModel productsData = new ProductsDataModel();
        // GET: Monitor
        [HttpGet]
        public ActionResult Index()
        {
            return View(productsData.GetProductByCategory("Monitor",10));
        }
        [HttpGet]
        public ActionResult MonitorBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productsData.GetProductByCategoryAndBrand("Monitor",brand,10));
        }
    }
}