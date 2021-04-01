using System;
using System.Collections.Generic;
using System.Globalization;
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
        public List<PurchaseDataViewModel> GetPurchaseVisualizationData(int id, int startYear, int endYear)
        {
        var result1 = (from s in data.Sales_Log
                          join p in data.Products on s.ProductId equals p.Id
                          where s.UserId == id
                          select new PurchaseDataViewModel
                          {
                              DatePurchased = startYear,
                              LaptopQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Laptop" && s.DateSold.Year == startYear  select s.Quantity).FirstOrDefault(),
                              SSDQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "SSD" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              RamQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Ram" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              MonitorQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Monitor" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              MouseQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Mouse" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              KeyboardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Keyboard" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              GraphicsCardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Graphics Card" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              CoolingFanQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Cooling Fan" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              CasingQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Casing" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              CPUQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "CPU" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault(),
                              MotherboardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "CPU" && s.DateSold.Year == startYear select s.Quantity).FirstOrDefault()
                          }).Distinct().FirstOrDefault();

            var result2 = (from s in data.Sales_Log
                          join p in data.Products on s.ProductId equals p.Id
                          where s.UserId == id
                          select new PurchaseDataViewModel
                          {
                              DatePurchased=endYear,
                              LaptopQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Laptop" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              SSDQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "SSD" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              RamQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Ram" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              MonitorQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Monitor" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              MouseQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Mouse" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              KeyboardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Keyboard" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              GraphicsCardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Graphics Card" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              CoolingFanQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Cooling Fan" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              CasingQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Casing" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              CPUQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "CPU" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault(),
                              MotherboardQuantity = (from s in data.Sales_Log join p in data.Products on s.ProductId equals p.Id where p.Category == "Mother Board" && s.DateSold.Year == endYear select s.Quantity).FirstOrDefault()
                          }).Distinct().FirstOrDefault();

            var result = new List<PurchaseDataViewModel>();
            result.Add(result1);
            result.Add(result2);
            return result;
        }
    }
}