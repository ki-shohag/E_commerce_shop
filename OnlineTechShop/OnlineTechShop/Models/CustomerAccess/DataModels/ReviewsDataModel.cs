using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.CustomerAccess.ViewModels;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class ReviewsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public List<ReviewsViewModel> GetReviewByProductId(int id)
        {
            var result = (from r in data.Reviews join c in data.Customers on r.UserId equals c.Id
                          where r.ProductId==id
                          select new ReviewsViewModel
                          {
                              ProductId = r.Product.Id,
                              CustomerId = r.Customer.Id,
                              CustomerName = r.Customer.FullName,
                              DatePosted = r.DatePosted,
                              Body = r.Body
                          });
            return result.ToList();
        }
        public void PostReview(Models.Review review)
        {
            data.Reviews.Add(review);
            data.SaveChanges();
        }
    }
}