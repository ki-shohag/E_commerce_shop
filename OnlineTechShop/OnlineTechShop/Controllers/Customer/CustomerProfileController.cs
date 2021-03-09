using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.DataModels;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerProfileController : Controller
    {
        CustomerDataModel customerData = new CustomerDataModel();
        // GET: CustomerProfile
        [HttpGet]
        public ActionResult Index()
        {
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
        }
        [HttpPost]
        public ActionResult Edit(int id, Models.Customer customer)
        {
            if (customerData.GetCustomerByEmail((string)customer.Email)!=null && customer.Email!=(string)Session["user_email"] && !customer.Email.Equals((string)Session["user_email"]))
            {

                TempData["msg"] = "Email address already registered!";
                return RedirectToAction("Edit", "CustomerProfile", id);
            }
            else if (customerData.GetCustomerByUserName((string)customer.UserName) != null && customer.UserName != (string)Session["user_name"] && !customer.UserName.Equals((string)Session["user_name"]))
            {
                TempData["msg"] = "User Name already registered!";
                return RedirectToAction("Edit", "CustomerProfile", id);
            }
            else
            {
                Models.Customer newCustomer = customerData.GetCustomerById(id);
                newCustomer.FullName = customer.FullName;
                newCustomer.Address = customer.Address;
                newCustomer.Phone = customer.Phone;
                newCustomer.Email = customer.Email;
                customerData.UpdateCustomer(newCustomer);
                TempData["msg"] = "Profile Updated!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(int id, FormCollection collection)
        {
            if (!collection["newPassword"].Equals(collection["confirmNewPassword"]))
            {
                TempData["msg"] = "Passwords did not match!";
                return RedirectToAction("ChangePassword", "CustomerProfile", id);
            }
            else if (collection["newPassword"] == null)
            {
                TempData["msg"] = "New password cannot be empty!";
                return RedirectToAction("ChangePassword", "CustomerProfile", id);
            }
            else if (collection["confirmNewPassword"] == null)
            {
                TempData["msg"] = "Confirm password cannot be empty!";
                return RedirectToAction("ChangePassword", "CustomerProfile", id);
            }
            else
            {
                CustomerDataModel customerData = new CustomerDataModel();
                Models.Customer customer = customerData.GetCustomerById(id);
                customer.Password = collection["newPassword"];
                customerData.UpdateCustomer(customer);
                TempData["msg"] = "Password reset was successfull!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult PurchaseHistory(int? id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult WishList(int? id)
        {
            return View();
        }
    }
}