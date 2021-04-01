using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class PurchaseListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();

        public ActionResult Index(FormCollection collection)
        {
            if (collection["fromDate"] != null && collection["toDate"] != null)
            {
                var purchases = context.Purchase_Log.ToList().Where(p => Convert.ToDateTime(p.PurchasedDate) >= Convert.ToDateTime(collection["fromDate"]) && Convert.ToDateTime(p.PurchasedDate) <= Convert.ToDateTime(collection["toDate"]));
                return View(purchases);
            }
            else
            {
                return View(context.Purchase_Log.ToList());
            }
        }
    }
}