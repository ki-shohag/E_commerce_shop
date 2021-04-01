using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentHomeController : Controller
    {
        OldProductDataModel oldProductData = new OldProductDataModel();

        public ActionResult Index(FormCollection collection)
        {
            if (collection["sortBy"] == "Price")
            {
                ViewBag.SelectedOption = "Price";
                ViewBag.OldProducts = oldProductData.GetAllOldProducts().OrderByDescending(x => x.BuyingPrice).ToList();
            }
            else if (collection["sortBy"] == "Quantity")
            {
                ViewBag.SelectedOption = "Quantity";
                ViewBag.OldProducts = oldProductData.GetAllOldProducts().OrderByDescending(x => x.Quantity).ToList();
            }
            else
            {
                ViewBag.OldProducts = oldProductData.GetAllOldProducts().ToList();
            }
            return View();
        }
        public ActionResult RejectOldProduct(int id)
        {
            OldProductDataModel oldProductData = new OldProductDataModel();
            Models.Old_Products oldProduct = oldProductData.GetOldProductById(id);
            oldProduct.Status = (string)"Rejected";
            oldProductData.UpdateOldProduct(oldProduct);
            TempData["msg"] = "Successfully rejected " + oldProduct.ProductName;
            return RedirectToAction("Index");
        }

        public ActionResult AcceptOldProduct(int id)
        {
            OldProductDataModel oldProductData = new OldProductDataModel();
            Models.Old_Products oldProduct = oldProductData.GetOldProductById(id);
            oldProduct.Status = (string)"Accepted";
            oldProductData.UpdateOldProduct(oldProduct);
            oldProductData.AddOldProductToPurchaseLog(oldProduct);
            TempData["msg"] = oldProduct.ProductName + "is added into purchase log";
            return RedirectToAction("Index");
        }



    }
}