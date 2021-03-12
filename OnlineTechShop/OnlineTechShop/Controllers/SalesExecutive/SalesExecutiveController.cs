using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;

namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveController : Controller
    {
        private TechShopDbEntities context = new TechShopDbEntities();
        
        // GET: SalesExecutive
        public ActionResult Index()
        {
            ViewData["SalesExecutive"] = context.SalesExecutives.ToList();
            return View(context.SalesExecutives.ToList());
        }
        public ActionResult Products()
        {
            ViewData["SalesExecutive"] = context.SalesExecutives.ToList();
            return View();
        }
    }
}