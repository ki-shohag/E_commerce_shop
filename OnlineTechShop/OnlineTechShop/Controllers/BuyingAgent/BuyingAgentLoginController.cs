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
            if(collection["email"] == "" &&  collection["password"] == "")
            {
                TempData["msg"] = "Empty email and Password field!";
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            else if(collection["email"] == "")
            {
                TempData["msg"] = "Empty email field!";
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            else if(collection["password"] == "")
            {
                TempData["msg"] = "Empty password field!";
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            else
            {
                BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
                Models.BuyingAgent buyingAgent = buyingAgentData.GetValidBuyingAgent(collection["email"], collection["password"]);
                if (buyingAgent != null)
                {

                    Session["buyingAgent_name"] = buyingAgent.UserName;
                    Session["buyingAgent_email"] = buyingAgent.Email;
                    Session["buyingAgent_id"] = buyingAgent.Id;
                    Session["buyingAgent_profile_pic"] = buyingAgent.ProfilePic;
                    Session["flag"] = true;
                    return RedirectToAction("Index", "BuyingAgentHome");
                }
                else
                {
                    TempData["msg"] = "Unregistered Credentials!";
                    return RedirectToAction("Index", "BuyingAgentLogin");
                }
            }
        }
    }
}