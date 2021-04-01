using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.AdminModel.AdminDataModel
{
    public class CustomerDataModel
    {
        TechShopDbEntities context = new TechShopDbEntities();

        public List<Customer> GetActiveCustomers()
        {
            List<Customer> customers = context.Customers.Where(x => x.Status == "Active").ToList();
            return customers;
        }

        public void BlockCustomer(int id)
        {
            var customer = context.Customers.Find(id);
            customer.Status = "Restricted";
            context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<Customer> GetRestrictedCustomers()
        {
            List<Customer> customers = context.Customers.Where(x => x.Status == "Restricted").ToList();
            return customers;
        }

        public void ReactivateCustomer(int id)
        {
            var customer = context.Customers.Find(id);
            customer.Status = "Active";
            context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}