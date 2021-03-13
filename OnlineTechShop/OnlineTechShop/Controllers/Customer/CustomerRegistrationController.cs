using System;
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
                OnlineTechShop.Models.CustomerAccess.DataModels.OrdersDataModel ordersData = new Models.CustomerAccess.DataModels.OrdersDataModel();
                if (customerData.GetCustomerByUserName(customer.UserName)!=null)
                {
                    TempData["msg"] = "User Name is alredy registered!";
                    return RedirectToAction("Index", "CustomerRegistration");
                }
                else if (customerData.GetCustomerByEmail(customer.Email)!=null)
                {
                    TempData["msg"] = "Email is alredy registered!";
                    return RedirectToAction("Index", "CustomerRegistration");
                }
                else
                {
                    customerData.InsertCustomer(customer);
                    Models.OrderData order = new OrderData();
                    string email = customerData.GetCustomerByEmail(customer.Email).Email.ToString();
                    ordersData.AddOrderDataOnCustomerRegistration(email);
                    TempData["msg"] = "Registration was successful!\nWant to login?";
                    return RedirectToAction("Index", "CustomerLogin");
                }
            }
            else
            {
                return View();
            }
        }
    }
}