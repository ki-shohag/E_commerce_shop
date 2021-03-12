using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Discount { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}