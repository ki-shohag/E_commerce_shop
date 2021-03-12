using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class ComponentsController : Controller
    {
        // GET: Components
        ProductsDataModel productData = new ProductsDataModel();
        [HttpGet]
        public ActionResult Index()
        {
            return View(productData.GetAllProducts());
        }
        [HttpGet]
        public ActionResult Ram()
        {
            return View(productData.GetProductByCategory("Ram", 10));
        }
        [HttpGet]
        public ActionResult RamBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Ram",brand,10));
        }
        [HttpGet]
        public ActionResult SSD()
        {
            return View(productData.GetProductByCategory("SSD",10));
        }
        [HttpGet]
        public ActionResult SSDBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("SSD", brand, 10));
        }
        [HttpGet]
        public ActionResult GraphicsCard()
        {
            return View(productData.GetProductByCategory("Graphics Card", 10));
        }
        [HttpGet]
        public ActionResult GraphicsCardBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Graphics Card", brand, 10));
        }
        [HttpGet]
        public ActionResult Casing()
        {
            return View(productData.GetProductByCategory("Casing", 10));
        }
        [HttpGet]
        public ActionResult CasingBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Casing", brand, 10));
        }
        [HttpGet]
        public ActionResult Motherboard()
        {
            return View(productData.GetProductByCategory("Mother Board", 10));
        }
        [HttpGet]
        public ActionResult MotherboardBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Motherboard", brand, 10));
        }
        [HttpGet]
        public ActionResult CPU()
        {
            return View(productData.GetProductByCategory("CPU", 10));
        }
        [HttpGet]
        public ActionResult CPUBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("CPU", brand, 10));
        }
        [HttpGet]
        public ActionResult HDD()
        {
            return View(productData.GetProductByCategory("HDD", 10));
        }
        [HttpGet]
        public ActionResult HDDBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("HDD", brand, 10));
        }
        [HttpGet]
        public ActionResult CoolingFan()
        {
            return View(productData.GetProductByCategory("Cooling Fan", 10));
        }
        [HttpGet]
        public ActionResult CoolingFanBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View(productData.GetProductByCategoryAndBrand("Cooling Fan", brand, 10));
        }
    }
}