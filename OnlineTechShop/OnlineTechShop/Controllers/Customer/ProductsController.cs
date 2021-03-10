using OnlineTechShop.Models.CustomerAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}