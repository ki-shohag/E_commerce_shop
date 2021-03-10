using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentLoginController : Controller
    {
        // GET: BuyingAgentLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
            Models.BuyingAgent buyingAgent = buyingAgentData.GetValidBuyingAgent(collection["email"], collection["password"]);
            if (buyingAgent != null)
            {

                Session["buyingAgent_name"] = buyingAgent.UserName;
                Session["buyingAgent_email"] = buyingAgent.Email;
                Session["buyingAgent_id"] = buyingAgent.Id;
                return RedirectToAction("Index", "BuyingAgentProfile");    
            }
            else
            {
                TempData["msg"] = "*Invalid email or password!";
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
        }
    }
}