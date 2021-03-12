using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.AdminModel.AdminViewModel
{
    public class AdminMetaData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }

        public string ProfilePic { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}