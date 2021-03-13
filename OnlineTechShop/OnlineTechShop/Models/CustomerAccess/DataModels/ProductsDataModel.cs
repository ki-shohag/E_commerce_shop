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
        public int GetProductStockQuantity(int id)
        {
            return data.Products.Find(id).Quantity;
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
        public List<Product> GetProductByCategory(string category, int limit)
        {
            return data.Products.Where(x => x.Category == category && x.Status == "In Stock").Take(limit).ToList();
        }
        public List<Product> GetProductByCategoryAndBrand(string category, string brand, int limit)
        {
            return data.Products.Where(x => x.Category == category && x.Brand==brand && x.Status == "In Stock").Take(limit).ToList();
        }
        public List<String> GetAllProductCategory()
        {
            return data.Products.Select(x => x.Category).Distinct().ToList();
        }
        public List<String> GetAllProductBrandsByCategory(string category)
        {
            return data.Products.Where(x=>x.Category==category).Select(x => x.Brand).Distinct().ToList();
        }
        public List<string> GetAllProductsKeyValue()
        {
            //var AllProducts = data.Products.ToList();
            //var ProductList = new List<KeyValuePair<string, int>>();
            //foreach (var key in AllProducts )
            //{
            //        ProductList.Add(new KeyValuePair<string, int>(key.ProductName, key.Id));

            //}
            //return ProductList;
            return data.Products.Where(x => x.Status == "In Stock").Select(x => x.ProductName).ToList();
        }
        public int GetProductIdByProductName(string name)
        {
            return (int)data.Products.Where(x => x.ProductName == name).Select(x => x.Id).FirstOrDefault();
        }
    }
}