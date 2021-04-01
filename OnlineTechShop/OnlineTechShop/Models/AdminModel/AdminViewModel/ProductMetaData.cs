using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.AdminModel.AdminViewModel
{
    public class ProductMetaData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required!")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Too many characters!")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [DataType(DataType.Text)]
        [StringLength(2000, ErrorMessage = "Too many characters!")]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Buying Price is required!")]
        [Display(Name = "Buying Price")]
        [Range(0, 999999999, ErrorMessage = "Invalid Entry")]
        public decimal BuyingPrice { get; set; }

        [Required(ErrorMessage = "Selling Price is required!")]
        [Display(Name = "Selling Price")]
        [Range(0,999999999,ErrorMessage ="Invalid Entry")]
        public decimal SellingPrice { get; set; }

        [Required(ErrorMessage = "Category is required!")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Brand is required!")]
        public string Brand { get; set; }
        
        public string Features { get; set; }

        [Required(ErrorMessage = "Quantity is required!")]
        [Range(0, 999999999, ErrorMessage = "Invalid Entry")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product image is required.")] 
        public string Images { get; set; }

        [Range(0, 999999999, ErrorMessage = "Invalid Entry")]
        public Nullable<int> Discount { get; set; }

        [Range(0, 999999999, ErrorMessage = "Invalid Entry")]
        public Nullable<int> Tax { get; set; }


        public Nullable<System.DateTime> DateAdded { get; set; }


        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}