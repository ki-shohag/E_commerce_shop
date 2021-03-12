using OnlineTechShop.Models.AdminModel.AdminDataModel;
using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class AdminLoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }
        
        [HttpPost]
        public ActionResult Index(Models.Admin model)
        {
            if (ModelState.IsValid)
            {
                AdDataModel adminData = new AdDataModel();
                Models.Admin admin = adminData.GetValidity(model);
                if (admin != null)
                {
                    Session["userName"] = admin.UserName;
                    Session["email"] = admin.Email;
                    Session["id"] = admin.Id;
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    TempData["msg"]="Please enter your Email and Password.";
                    return RedirectToAction("Index", "AdminLogin");
                }
            }
            TempData["msg"] = "Empty";
            return View("Login");
        }
    }
}