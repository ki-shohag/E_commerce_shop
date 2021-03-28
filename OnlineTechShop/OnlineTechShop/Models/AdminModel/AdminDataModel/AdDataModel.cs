using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Models.AdminModel.AdminDataModel
{
    public class AdDataModel
    {
        TechShopDbEntities context = new TechShopDbEntities();
        
        public Admin GetValidity(Admin model)
        {
            return context.Admins.Where(m => m.Email == model.Email && m.Password == model.Password).FirstOrDefault();
        }

        public Admin GetAdminByEmail(string email)
        {
            Admin admin = context.Admins.Where(m => m.Email == email).FirstOrDefault();
            return admin;
        }

        
    }
}