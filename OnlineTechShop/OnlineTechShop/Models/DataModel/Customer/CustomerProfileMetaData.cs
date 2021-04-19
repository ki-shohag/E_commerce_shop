using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.DataModel.Customer
{
    public class CustomerProfileMetaData
    {
        [Required, MinLength(3), MaxLength(20)]
        public string FullName { get; set; }
        [Required, MinLength(10), MaxLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(11), MaxLength(11)]
        public string Phone { get; set; }
        [Required, MinLength(3), MaxLength(100)]
        public string Address { get; set; }
        [Required, MinLength(3), MaxLength(20)]
        public string UserName { get; set; }
    }
}