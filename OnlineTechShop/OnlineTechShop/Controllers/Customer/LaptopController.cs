using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LaptopBrand(string brand)
        {
            ViewBag.brand = brand;
            return View();
        }
        [HttpGet]
        public ActionResult ShowLaptop(int? id)
        {
            return View();
        }

    }
}