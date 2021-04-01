using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class ProductReviewsController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();

        public ActionResult ProductReview(int id)
        {
           return View(context.Reviews.Where(r => r.ProductId == id).ToList());
        }
    }
}