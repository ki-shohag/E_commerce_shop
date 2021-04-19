using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class ProductsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        RatingsDataModel RatingsData = new RatingsDataModel();
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
            return data.Products.OrderByDescending(x => x.Id).Take(6).ToList();
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
            return data.Products.Select(x => x.ProductName).ToList();
        }
        public int GetProductIdByProductName(string name)
        {
            return (int)data.Products.Where(x => x.ProductName == name).Select(x => x.Id).FirstOrDefault();
        }
        public decimal GetBuyingPriceByProductId(int id)
        {
            return data.Products.Where(x => x.Id == id).Select(x => x.BuyingPrice).FirstOrDefault();
        }
        public void UpdateProductQuantityOnPurchase(int id, int quantity)
        {
            Models.Product p = new Product();
            p = GetProductById(id);
            p.Quantity -= quantity;
            if (p.Quantity==0)
            {
                p.Status = "Out of Stock";
            }
            data.Entry(p).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }
        public List<Models.Product> GetRecentlyViewedProducts(List<int> ProductsIdList)
        {
            List<Models.Product> products = new List<Product>();
            int i = 0;
            foreach (var item in ProductsIdList.Distinct())
            {
                i++;
                products.Add(GetProductById(item));
                if (i>2)
                {
                    break;
                }
                
            }
            return products;
        }
        public IQueryable GetPurchasedProductData(int id)
        {
            var result = (from p in data.Products join s in data.Sales_Log on p.Id equals s.ProductId
                          where s.UserId ==id select new { 
                              ID = p.Id,
                              ProductName = p.ProductName,
                              Price = p.SellingPrice,
                              Tax = p.Tax,
                              Category = p.Category
                          });
            return result;
        }
    }
}