using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductStatus { get; set; }
        public decimal BuyingPrice { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Features { get; set; }
        public int Quantity { get; set; }
        public string Images { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public double AvgRating { get; set; }
    }
}