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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ram()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RamBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult SSD()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SSDBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult GraphicsCard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GraphicsCardBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult Casing()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CasingBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult Motherboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MotherboardBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult CPU()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CPUBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult HDD()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HDDBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult CoolingFan()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CoolingFanBrand(string brand)
        {
            ViewBag.Brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult ShowComponent(int? id)
        {
            return View();
        }
    }
}