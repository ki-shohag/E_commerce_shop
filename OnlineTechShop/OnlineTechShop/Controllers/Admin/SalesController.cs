using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class SalesController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();

        public ActionResult Index(FormCollection collection)
        {
            if (collection["fromDate"] != null && collection["toDate"] != null)
            {
                var sales = context.Sales_Log.ToList().Where(p => Convert.ToDateTime(p.DateSold) >= Convert.ToDateTime(collection["fromDate"]) && Convert.ToDateTime(p.DateSold) <= Convert.ToDateTime(collection["toDate"]));


                return View(sales);
            }
            else
            {
                return View(context.Sales_Log.ToList());
            }
        }

        public ActionResult GetDataCategory()
        {

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Category)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataProductName()
        {

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.ProductName)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}