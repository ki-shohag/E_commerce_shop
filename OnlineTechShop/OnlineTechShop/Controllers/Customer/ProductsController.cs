using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace OnlineTechShop.Controllers.Customer
{
    public class ProductsController : Controller
    {
        // GET: Products
        ProductsDataModel productsData = new ProductsDataModel();
        ReviewsDataModel reviewsData = new ReviewsDataModel();
        RatingsDataModel ratingsData = new RatingsDataModel();
        WishListsDataModel wishData = new WishListsDataModel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShowProduct(int id)
        {
            if (Request.Cookies["RecentlyViewedProductsListCookie"] != null)
            {
                List<int> RecentlyViewedProductsList = Request.Cookies["RecentlyViewedProductsListCookie"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                RecentlyViewedProductsList.Insert(0,id);
                var RecentlyViewedProductsListString = String.Join(",", RecentlyViewedProductsList);
                HttpCookie RecentlyViewedProductsListCookie = new HttpCookie("RecentlyViewedProductsListCookie", RecentlyViewedProductsListString);
                Response.Cookies.Add(RecentlyViewedProductsListCookie);
                RecentlyViewedProductsListCookie.Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                List<int> RecentlyViewedProductsList = new List<int>();
                RecentlyViewedProductsList.Insert(0,id);
                var RecentlyViewedProductsListString = String.Join(",", RecentlyViewedProductsList);
                HttpCookie RecentlyViewedProductsListCookie = new HttpCookie("RecentlyViewedProductsListCookie", RecentlyViewedProductsListString);
                Response.Cookies.Add(RecentlyViewedProductsListCookie);
                RecentlyViewedProductsListCookie.Expires = DateTime.Now.AddDays(7);
            }

            ViewBag.ReviewsList = reviewsData.GetReviewByProductId(id);
            ViewBag.Rating = ratingsData.GetProductRatingByProductId(id);
            ViewBag.RatingData = ratingsData.GetRatingDataByProductId(id);
            ViewBag.UserId = Session["user_id"];
            return View(productsData.GetProductById(id));
        }
        [HttpPost]
        public ActionResult PostReview(int id, Models.Review review)
        {
            review.UserId = (int)Session["user_id"];
            review.ProductId = id;
            review.DatePosted = DateTime.Now;
            if (ModelState.IsValid)
            {
                reviewsData.PostReview(review);
                return Redirect("/Products/ShowProduct/"+review.ProductId);
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public JsonResult PostRating(Models.Rating data)
        {
            Models.Rating newRating = new Models.Rating();
            newRating.Rating1 = Convert.ToInt32(data.Rating1);
            newRating.UserId = (int)data.UserId;
            newRating.ProductId = (int)data.ProductId;
            newRating.DateRated = DateTime.Now.Date;
            ratingsData.PostNewRating(newRating);
            return Json(newRating, JsonRequestBehavior.AllowGet);
        }
        public void AddToWishList(int id)
        {
            if (Session["user_id"]==null || Session["user_id"].Equals(null))
            {
                TempData["msg"] = "Please login first or sign up!";
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            
            Models.WishList wishedItem = new Models.WishList();
            wishedItem.CustomerId = (int)Session["User_id"];
            wishedItem.ProductId = id;
            wishedItem.DateWished = DateTime.Now.Date;
            if (wishData.CheckValidWish(wishedItem.CustomerId, wishedItem.ProductId))
            {
                wishData.AddProductToWishList(wishedItem);
                TempData["msg"] = "Added product to wish list! Check your wish list from account info.";
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                TempData["msg"] = "Product is already in your wish list! Visit your account to check wish list.";
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult RemoveFromWishList(int id)
        {
            wishData.RemoveProductFromWishList(id);
            TempData["msg"] = "Removed product from your wish list!";
            return Redirect("/CustomerProfile/WishList/"+(int)Session["user_id"]);
        }
        [HttpPost]
        public ActionResult GetProductsByCategory(Models.Product data)
        {
            var MyList = new List<KeyValuePair<string, int>>();
            var list = productsData.GetProductByCategory(data.Category, (int)data.Discount);
            foreach (var item in list)
            {
                MyList.Add(new KeyValuePair<string, int>(item.ProductName, item.Id));
            }
            return Json(new JavaScriptSerializer().Serialize(MyList));
        }
        [HttpPost]
        public ActionResult GetProductById(Models.Product data)
        {
            Models.Product product = new Models.Product();
            product = productsData.GetProductById(data.Quantity);
            if (product!=null)
            {
                return Json(JsonConvert.SerializeObject(product), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { Id = 0}), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetAllProductsKeyValue(string data)
        {
            return Json(new JavaScriptSerializer().Serialize(productsData.GetAllProductsKeyValue()));
        }
        [HttpPost]
        public ActionResult GetProductIdByProductName(string name)
        {
            return Json(new JavaScriptSerializer().Serialize(productsData.GetProductIdByProductName(name)));
        }
    }
}