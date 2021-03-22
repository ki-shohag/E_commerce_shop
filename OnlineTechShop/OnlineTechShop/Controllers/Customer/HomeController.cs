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
            if (Request.Cookies["RecentlyViewedProductsListCookie"] != null)
            {
                List<int> RecentlyViewedProductsList = Request.Cookies["RecentlyViewedProductsListCookie"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                ViewBag.RecentlyViewedProducts = productData.GetRecentlyViewedProducts(RecentlyViewedProductsList);
            }
            else
            {
                ViewBag.RecentlyViewedProducts = null;
            }
            ViewBag.FeaturedProducts = productData.GetFeaturedProducts();
            ViewBag.UpComingProducts = productData.GetUpComingProducts();
            return View();
        }
    }
}