using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class AdminDashboardController : AdminBaseController
    {
        // GET: AdminDashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataCategory()
        {
            TechShopDbEntities context = new TechShopDbEntities();

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Category)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).OrderByDescending(x => x.count).ToList().Take(7);
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataBrand()
        {
            TechShopDbEntities context = new TechShopDbEntities();

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Brand)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).OrderByDescending(x => x.count).ToList().Take(7);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }


}