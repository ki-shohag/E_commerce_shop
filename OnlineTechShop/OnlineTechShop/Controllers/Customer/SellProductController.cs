using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Customer
{
    public class SellProductController : Controller
    {
        OldProudctsDataModel oldProductsData = new OldProudctsDataModel();
        // GET: SellProduct
        public ActionResult Index()
        {
            if (Session["user_email"]==null)
            {
                TempData["msg"] = "Please login first";
                return RedirectToAction("Index", "CustomerLogin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Old_Products oldProduct)
        {
            oldProduct.CutomerId = (int)Session["user_id"];
            oldProduct.BuyingPrice = 0;
            oldProduct.Source = "Customer";
            oldProduct.Status = "Pending";
            if (ModelState.IsValid)
            {
                oldProductsData.InsertOldProudctForSale(oldProduct);
                TempData["msg"] = "Item posted for sell!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Failed to post!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult PendingSellPosts()
        {
            return View(oldProductsData.GetPendingOldProductsByCustomerId((int)Session["user_id"]));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(oldProductsData.GetPostedProudctById(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Models.Old_Products oldProduct)
        {
            oldProduct.Id = id;
            oldProduct.CutomerId = (int)Session["user_id"];
            oldProduct.BuyingPrice = 0;
            oldProduct.Source = "Customer";
            oldProduct.Status = "Pending";

            if (ModelState.IsValid)
            {
                oldProductsData.UpdatePost(oldProduct);
                TempData["msg"] = "Post has been updated!";
                return RedirectToAction("PendingSellPosts");
            }
            else
            {
                TempData["msg"] = "Failed to update post!";
                return RedirectToAction("PendingSellPosts");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            oldProductsData.DeletePost(id);
            TempData["msg"] = "Deleted post!";
            return RedirectToAction("PendingSellPosts");
        }
        [HttpGet]
        public ActionResult SoldProducts()
        {
            return View(oldProductsData.GetSoldProductsByCustomerId((int)Session["user_id"]));
        }
    }
}