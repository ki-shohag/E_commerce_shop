using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.CustomerAccess.DataModels;
namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Profile_Pic { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<CustomerViewModel> ShowAllCustomers()
        {
            CustomerDataModel customerData = new CustomerDataModel();
            var customers = customerData.GetAllCustomers();
            List<CustomerViewModel> customersList = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                customersList.Add(customerData.ConvertToCustomerViewModel(customer)); 
            }
            return customersList;
        }
    }
}