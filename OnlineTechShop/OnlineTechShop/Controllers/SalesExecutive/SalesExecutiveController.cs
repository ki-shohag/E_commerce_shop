using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;
using OnlineTechShop.Models.CustomerAccess.DataModels;

namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveController : Controller
    {
        private TechShopDbEntities context = new TechShopDbEntities();
        
        // GET: SalesExecutive
        public ActionResult Index()
        {
           
            return View(context.SalesExecutives.ToList());
        }
        public ActionResult Products()
        {
           
            return View(context.Products.ToList());
        }
        public ActionResult AvailableProducts()
        {
             ProductsDataModel data = new ProductsDataModel();
            return View(data.GetAvailableProducts());
        }
        public ActionResult UpcomingProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            return View(data.GetUpComingProducts());
        }

        public ActionResult DiscountProducts()
        {
            ProductsDataModel data = new ProductsDataModel();
            return View(data.GetAllDiscountProducts());
        }
        public JsonResult SearchByCategory(string SearchValue)
        {
            ProductsDataModel data = new ProductsDataModel();
            return Json(data.GetProductByCategory(SearchValue, 1), JsonRequestBehavior.AllowGet);
        }

    }
}