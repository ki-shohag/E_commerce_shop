using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class AccessoriesController : Controller
    {
        ProductsDataModel productData = new ProductsDataModel();
        [HttpGet]
        public ActionResult Index()
        {
            return View(productData.GetAllProducts());
        }
        [HttpGet]
        public ActionResult Keyboard()
        {
            return View(productData.GetProductByCategory("Keyboard", 10));
        }
        [HttpGet]
        public ActionResult KeyboardBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Keyboard", brand, 10));
        }
        [HttpGet]
        public ActionResult Mouse()
        {
            return View(productData.GetProductByCategory("Mouse", 10));
        }
        [HttpGet]
        public ActionResult MouseBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Mouse", brand, 10));
        }
    }
}