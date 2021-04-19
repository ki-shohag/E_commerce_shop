using OnlineTechShop.Models;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class ContactCustomerController : Controller
    {
        // GET: ContactCustomer
        public ActionResult Index(int id)
        {
            if (Session["flag"] == null)
            {
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            TempData["customerId"] = (int)id;
            CustomerDataModel customers = new CustomerDataModel();
            Models.Customer customer = customers.GetCustomerById(id);
            return View(customer);
        }

        public ActionResult GetCustomerwisePurchase(int id)
        {
            if (Session["flag"] == null)
            {
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            TechShopDbEntities data = new TechShopDbEntities();
            List<Purchase_Log> buyingData = data.Purchase_Log.Where(x => x.CustomerId == id).OrderBy(x => x.Id).ToList();
            return View(buyingData);
        }

        [HttpGet]
        public ActionResult ExportHelper()
        {
            TechShopDbEntities data = new TechShopDbEntities();
            List<Purchase_Log> buyingData = data.Purchase_Log.Where(x => x.CustomerId == (int)TempData["customerId"]).OrderBy(x => x.Id).ToList();
            return View(buyingData);
        }

        [HttpGet]
        public ActionResult ExportPdf()
        {
            var report = new ActionAsPdf("ExportHelper");
            return report;
        }

        public ActionResult Contact()
        {
            if (Session["flag"] == null)
            {
                return RedirectToAction("Index", "BuyingAgentLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection collection)
        {
            if(collection["message"] == "")
            {
                TempData["msg"] = "Write something..";
            }
            else
            {
                TempData["msg"] = "Message sent through mail...";
            }
            return RedirectToAction("Contact");
        }
    }
}