using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class MonitorController : Controller
    {
        // GET: Monitor
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MonitorBrand(string brand)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShowMonitor(int? id)
        {
            return View();
        }
    }
}