using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.DataModels;

namespace OnlineTechShop.Controllers.Customer
{
    public class LaptopController : Controller
    {
        ProductsDataModel productData = new ProductsDataModel();
        // GET: Laptop
        [HttpGet]
        public ActionResult Index()
        {
            return View(productData.GetProductByCategory("Laptop",10));
        }

        [HttpGet]
        public ActionResult LaptopBrand(string brand)
        {
            ViewBag.brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Laptop", brand, 10));
        }

    }
}