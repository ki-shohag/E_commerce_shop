using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models;


namespace OnlineTechShop.Models.Sales.DataModels
{
    public class SalesExecutiveDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
  
        public SalesExecutive GetValidSalesExecutive(string email, string password)
        {
            return data.SalesExecutives.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
      
    }
}