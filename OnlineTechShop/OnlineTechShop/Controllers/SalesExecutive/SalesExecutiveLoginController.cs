

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.Sales.DataModels;
using OnlineTechShop.Models;

namespace OnlineTechShop.Controllers.SalesExecutive
{
    public class SalesExecutiveLoginController : Controller
    {
        // GET: BuyingAgentLogin
        TechShopDbEntities data = new TechShopDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            // Models.BuyingAgent buyingAgent = buyingAgentData.GetValidBuyingAgent(collection["email"], collection["email"]);
            SalesExecutiveDataModel data = new SalesExecutiveDataModel();
            Models.SalesExecutive se = data.GetValidSalesExecutive(collection["email"], collection["password"]);

            if (se != null)
            {

               
                Session["seid"] = se.Id;
               
                return RedirectToAction("Index", "SalesExecutive");
            }
            else
            {
                TempData["msg"] = "*Invalid email or password!";
                return RedirectToAction("Index", "SalesExecutiveLogin");
            }
        }
    }
}