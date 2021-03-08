﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.ViewModels;
using OnlineTechShop.Models;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerRegistrationController : Controller
    {
        // GET: CustomerRegistration
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(OnlineTechShop.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                OnlineTechShop.Models.CustomerAccess.DataModels.CustomerDataModel customerData = new Models.CustomerAccess.DataModels.CustomerDataModel();
                customerData.InsertCustomer(customer);
                TempData["msg"] = "Registration was successful!\nWant to login?";
                return RedirectToAction("Index","CustomerLogin");
            }
            else
            {
                return View();
            }
        }
    }
}