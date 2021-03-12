using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.DataModels;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerLoginController : Controller
    {
        // GET: CustomerLogin
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            CustomerDataModel customerData = new CustomerDataModel();
            Models.Customer customer = customerData.GetValidCustomer(collection["email"], collection["password"]);
            if (customer!=null)
            {
                if (customer.Status=="Active")
                {
                    Session["user_name"] = customer.UserName;
                    Session["user_email"] = customer.Email;
                    Session["user_profile_pic"] = customer.ProfilePic;
                    Session["user_id"] = customer.Id;
                    Session["user_status"] = customer.Status;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "*You have been blocked to use the system!";
                    return RedirectToAction("Index", "CustomerLogin");
                }
            }
            else
            {
                TempData["msg"] = "*Invalid email or password!";
                return RedirectToAction("Index", "CustomerLogin");
            }
        }
    }
}