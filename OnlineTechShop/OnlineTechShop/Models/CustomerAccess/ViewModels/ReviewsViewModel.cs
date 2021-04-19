using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class ReviewsViewModel
    {
        public int ReviewId { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}