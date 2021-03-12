using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using System.IO;

namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerProfileController : Controller
    {
        CustomerDataModel customerData = new CustomerDataModel();
        WishListsDataModel wishedData = new WishListsDataModel();
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
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
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
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
        }

        [HttpGet]
        public ActionResult WishList(int id)
        {
            var list = wishedData.GetWishListByCustomerId(id);
            ViewBag.WishList = list;
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
        }
        [HttpPost]
        public ActionResult UploadProfilePic(HttpPostedFileBase image)
        {
            if (image.ContentLength>0)
            {
                Models.Customer customer =customerData.GetCustomerByEmail((string)Session["user_email"]);
                if (customer!=null)
                {
                    string ImageFileName = Path.GetFileName(image.FileName);
                    string ImageExtension = Path.GetExtension(image.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/Contents/Upload_Files/Customer/ProfilePic"), ImageFileName.Replace(ImageFileName, Session["user_email"].ToString() + ImageExtension));
                    image.SaveAs(FolderPath);
                    customer.ProfilePic = ImageFileName.Replace(ImageFileName, Session["user_email"].ToString() + ImageExtension);
                    Session["user_profile_pic"] = customer.ProfilePic;
                    customerData.UpdateCustomer(customer);
                    TempData["msg"] = "Successfully uploaded profile picture!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Failed to upload profile picture!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["msg"] = "Failed to upload profile picture!";
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult RemoveProfilePic()
        {
            Models.Customer customer = customerData.GetCustomerByEmail((string)Session["user_email"]);
            if (customer!=null)
            {
                customer.ProfilePic = null;
                Session["user_profile_pic"] = null;
                customerData.UpdateCustomer(customer);
                TempData["msg"] = "Removed profile picture!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Failed to remove profile picture!";
                return RedirectToAction("Index");
            }
        }
    }
}