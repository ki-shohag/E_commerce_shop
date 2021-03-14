using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class InvoiceViewModel
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerStatus { get; set; }
        public string ShippingMethod { get; set; }
        public string Phone { get; set; }
        public DateTime DateSold { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

    }
}