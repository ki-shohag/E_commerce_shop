using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentProfileController : Controller
    {
        BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
        
        public ActionResult Index()
        {
            return View(buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]));
        }
    }
}