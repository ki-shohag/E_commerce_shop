using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;
using OnlineTechShop.Models.CustomerAccess.DataModels;
namespace OnlineTechShop.Controllers.Customer
{
    public class HomeController : Controller
    {
        // GET: Home
        ProductsDataModel productData = new ProductsDataModel();
        public ActionResult Index()
        {
            ViewBag.FeaturedProducts = productData.GetFeaturedProducts();
            return View();
        }
    }
}