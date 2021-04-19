using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class OldProductListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        // GET: OldProductList
        public ActionResult Index()
        {
            return View(context.Old_Products.ToList());
        }
    }
}