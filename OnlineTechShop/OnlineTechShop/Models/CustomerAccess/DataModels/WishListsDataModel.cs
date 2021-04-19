using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.CustomerAccess.ViewModels;
namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class WishListsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        RatingsDataModel ratingData = new RatingsDataModel();
        public void AddProductToWishList(Models.WishList wishedItem)
        {
            data.WishLists.Add(wishedItem);
            data.SaveChanges();
        }
        public bool CheckValidWish(int customerId, int productId)
        {
            if (data.WishLists.Where(x=>x.CustomerId==customerId && x.ProductId==productId).FirstOrDefault()!=null)
            {
                return false;
            }
            return true;
        }
        public bool RemoveProductFromWishList(int id)
        {
            data.WishLists.Remove(data.WishLists.Find(id));
            data.SaveChanges();
            return true;
        }
        public List<WishListViewModel> GetWishListByCustomerId(int id)
        {
            var result = (from w in data.WishLists
                          join c in data.Customers on w.CustomerId equals c.Id
                          join p in data.Products on w.ProductId equals p.Id
                          where c.Id == id
                          select new WishListViewModel
                          {
                              ProductId = w.ProductId,
                              ProductName = p.ProductName,
                              Price = p.SellingPrice,
                              Image = p.Images,
                              Status = p.Status,
                              CustomerId = c.Id,
                              Discount = p.Discount,
                              Id = w.Id
                          }).Distinct();
            return result.ToList();
        }
    }
}