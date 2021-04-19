using OnlineTechShop.Models.BuyingAgentAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentPasswordController : Controller
    {
        // GET: BuyingAgentPassword
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
            Models.BuyingAgent buyingAgent = buyingAgentData.GetBuyingAgentByEmail(collection["email"]);
            if (buyingAgent != null)
            {
                TempData["msg"] = "*Request sent! Check your mail for further process.";
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            else
            {
                TempData["msg"] = "*Unregisterd email!";
                return RedirectToAction("Index", "BuyingAgentPassword");
            }
        }
    }
}