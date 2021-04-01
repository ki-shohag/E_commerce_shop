using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentLogOutController : Controller
    {
        // GET: BuyingAgentLogOut
        public ActionResult Index()
        {
            Session.Clear();
            return RedirectToAction("Index", "BuyingAgentLogin");
        }
    }
}