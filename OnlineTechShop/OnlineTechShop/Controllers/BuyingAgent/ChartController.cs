using OnlineTechShop.Models;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class ChartController : Controller
    {
        TechShopDbEntities data = new TechShopDbEntities();
        BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryChart()
        {
            var categoryNameQuantity = new List<KeyValuePair<string, int>>();
            var allPurchaseData = buyingAgentData.GetAllBuyingData().ToList();
            Dictionary<string, bool> check = new Dictionary<string, bool>();
            foreach (var item in allPurchaseData)
            {
                check.Add(item.Category, true);
            }
            foreach (KeyValuePair<string, bool> item in check)
            {
                string currCategory = item.Key;
                int totalQuantity = data.Purchase_Log.Where(x => x.Category == currCategory).Count();
                categoryNameQuantity.Add(new KeyValuePair<string, int>(currCategory, totalQuantity));
            }
            return Json(new JavaScriptSerializer().Serialize(categoryNameQuantity), JsonRequestBehavior.AllowGet);
        }
    }
}