using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.BuyingAgentAccess.DataModels
{
    public class OldProductDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();

        public List<Old_Products> GetAllOldProducts()
        {
            List<Old_Products> oldProducts = data.Old_Products.Where(x => x.Status == "Pending").OrderBy(x => x.Id).ToList();
            return oldProducts;
        }
        public Old_Products GetOldProductById(int id)
        {
            return data.Old_Products.Find(id);
        }

        public void UpdateOldProduct(Old_Products oldProduct)
        {
            data.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }

        public Purchase_Log ConvertToPurchaseLog(Models.Old_Products oldProducts)
        {
            Purchase_Log purchaseLog = new Purchase_Log();
            purchaseLog.CustomerId = oldProducts.CutomerId;
            purchaseLog.ProductName = oldProducts.ProductName;
            purchaseLog.ProductDescription = oldProducts.ProductDescription;
            purchaseLog.Status = oldProducts.Status;
            purchaseLog.BuyingPrice = oldProducts.BuyingPrice;
            purchaseLog.Category = oldProducts.Category;
            purchaseLog.Brand = oldProducts.Brand;
            purchaseLog.Features = oldProducts.Features;
            purchaseLog.Quantity = oldProducts.Quantity;
            purchaseLog.Images = oldProducts.Images;
            purchaseLog.PurchasedDate = DateTime.Now;
            return purchaseLog;
        }

        public void AddOldProductToPurchaseLog(Models.Old_Products oldProduct)
        {
            data.Purchase_Log.Add(ConvertToPurchaseLog(oldProduct));
            data.SaveChanges();
        }
    }
}