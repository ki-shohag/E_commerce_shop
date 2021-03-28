using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.AdminModel.AdminViewModel
{
    public class BuyingAgentMetaData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Too many characters!")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        [StringLength(20, ErrorMessage = "Too many characters!")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string ProfilePic { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Password is too weak.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter a valid email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Range(9999, 99999999999, ErrorMessage = "Enter a valid phone number.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [DataType(DataType.Text)]
        [StringLength(150, ErrorMessage = "Too many characters!")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is required!")]
        [Range(2000, 999999999, ErrorMessage = "Enter a valid amount")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}