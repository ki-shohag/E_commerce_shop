using OnlineTechShop.AuthData;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineTechShop.Controllers.Customer
{
    [CustomerAuthentication]
    public class SellProductController : Controller
    {
        OldProudctsDataModel oldProductsData = new OldProudctsDataModel();
        ProductsDataModel ProductsData = new ProductsDataModel();
        // GET: SellProduct
        public ActionResult Index()
        {
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
                return View();
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
                return View(oldProductsData.GetPostedProudctById(id));
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (oldProductsData.CheckPostStatusByPostId(id) == true)
            {
                TempData["msg"] = "Sorry your post is already been reviewed!\n It cannot be deleted now!";
                return RedirectToAction("PendingSellPosts");
            }
            else
            {
                oldProductsData.DeletePost(id);
                TempData["msg"] = "Deleted post!";
                return RedirectToAction("PendingSellPosts");
            }
        }
        [HttpGet]
        public ActionResult SoldProducts()
        {
            var model = oldProductsData.GetSoldProductsByCustomerId((int)Session["user_id"]);
            return View(model);
        }
        [HttpGet]
        public ActionResult GetMostSoldCategoriesData()
        {
            var MyList = new List<KeyValuePair<string, int>>();
            var list = oldProductsData.GetSoldProductsByCustomerId((int)Session["user_id"]).GroupBy(x=>x.Category);

            foreach (var group in list)
            {
                MyList.Add(new KeyValuePair<string, int>(group.Key, group.Count()));
            }
            
            return Json(new JavaScriptSerializer().Serialize(MyList), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPurchasedProductsData()
        {
            var list = ProductsData.GetPurchasedProductData((int)Session["user_id"]);
            return Json(new JavaScriptSerializer().Serialize(list), JsonRequestBehavior.AllowGet);
        }
    }
}