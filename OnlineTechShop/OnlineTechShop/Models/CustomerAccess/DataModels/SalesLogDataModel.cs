using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.CustomerAccess.ViewModels;
namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class SalesLogDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public void AddSalesLog(Models.Sales_Log sales_Log)
        {
            data.Sales_Log.Add(sales_Log);
            data.SaveChanges();
        }
        public List<PurchaseViewModel> GetPurchaseHistoryByCustomerId(int id)
        {
            var result = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id join c in data.Customers on s.UserId equals c.Id
                          where c.Id == id select new PurchaseViewModel { 
                              ProductId = p.Id,
                              ProductName = p.ProductName,
                              DatePurchased = s.DateSold,
                              Discount = s.Discount,
                              Tax = s.Tax,
                              Status = s.Status,
                              Quantity = s.Quantity,
                              TotalPrice = s.TotalPrice,
                              UnitPrice = p.SellingPrice,
                              PaymentType = null
                          });
            return result.ToList();
        }
    }
}