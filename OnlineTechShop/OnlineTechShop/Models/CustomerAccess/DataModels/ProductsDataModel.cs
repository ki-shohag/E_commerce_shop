using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class ProductsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public List<Product> GetAllProducts()
        {
            return data.Products.OrderByDescending(x=>x.Id).ToList();
        }
        public List<Product> GetAvailableProducts()
        {
            return data.Products.Where(x=>x.Status=="In Stock").OrderByDescending(x=>x.Id).ToList();
        }
        public List<Product> GetUpComingProducts()
        {
            return data.Products.Where(x=>x.Status=="Up Coming").OrderByDescending(x => x.Id).ToList();
        }
        public List<Product> GetFeaturedProducts()
        {
            return data.Products.OrderByDescending(x => x.Id).Take(10).ToList();
        }
        public List<Product> GetAllDiscountProducts()
        {
            return data.Products.Where(x=>x.Discount > 0 && x.Status=="In Stock" && x.Quantity > 0).OrderByDescending(x => x.Id).Take(10).ToList();
        }
        public Product  GetProductById(int id)
        {
            return data.Products.Find(id);
        }
    }
}