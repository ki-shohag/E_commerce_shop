using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class CompareProductsController : Controller
    {
        // GET: CompareProducts
        ProductsDataModel productsData = new ProductsDataModel();
        public ActionResult Index()
        {
            return View(productsData.GetAllProductCategory());
        }
    }
}