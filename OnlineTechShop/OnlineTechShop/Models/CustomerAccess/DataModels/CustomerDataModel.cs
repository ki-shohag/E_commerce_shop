using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models;
using OnlineTechShop.Models.CustomerAccess.ViewModels;


namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class CustomerDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = data.Customers.OrderBy(x => x.Id).ToList();
            return customers;
        }
        public CustomerViewModel ConvertToCustomerViewModel(Customer customer)
        {
            CustomerViewModel model = new CustomerViewModel();
            model.Id = customer.Id;
            model.FullName = customer.FullName;
            model.Email = customer.Email;
            model.Address = customer.Address;
            model.JoiningDate = customer.JoiningDate;
            model.LastUpdated = customer.LastUpdated;
            model.Password = customer.Password;
            model.Profile_Pic = customer.ProfilePic;
            model.Status = customer.Status;
            model.UserName = customer.UserName;
            return model;
        }
        public void InsertCustomer(Customer customer)
        {
            customer.JoiningDate = DateTime.Now;
            customer.LastUpdated = DateTime.Now;
            customer.Status = "Active";
            data.Customers.Add(customer);
            data.SaveChanges();
        }
        public Customer GetValidCustomer(string email, string password)
        {
            return data.Customers.Where(x=>x.Email==email && x.Password==password).FirstOrDefault();
        }
        public Customer GetCustomerByEmail(string email)
        {
            return data.Customers.Where(x => x.Email == email).FirstOrDefault();
        }
        public void UpdateCustomer(Customer customer)
        {
            data.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }
    }
}