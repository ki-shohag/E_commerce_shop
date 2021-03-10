using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class RatingsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public double GetProductRatingByProductId(int id)
        {
            return data.Ratings.Where(x=>x.ProductId==id).Average(x => x.Rating1);
        }
        public void PostNewRating(Models.Rating rating)
        {
            data.Ratings.Add(rating);
            data.SaveChanges();
        }
        public Models.Rating GetRatingDataByProductId(int id)
        {
            return data.Ratings.Find(id);
        }
    }
}