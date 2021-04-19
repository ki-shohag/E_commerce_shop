using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models.CustomerAccess.DataModels;
using System.IO;
using System.Web.Script.Serialization;
using OnlineTechShop.AuthData;
using System.Diagnostics;

namespace OnlineTechShop.Controllers.Customer
{
    [CustomerAuthentication]
    public class CustomerProfileController : Controller
    {
        CustomerDataModel customerData = new CustomerDataModel();
        WishListsDataModel wishedData = new WishListsDataModel();
        OrdersDataModel ordersData = new OrdersDataModel();
        SalesLogDataModel salesLogData = new SalesLogDataModel();
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
                Models.Customer currentCustomer = customer;
                Models.Customer newCustomer = customerData.GetCustomerById(id);

                customer = newCustomer;
                customer.FullName = currentCustomer.FullName;
                customer.Email = currentCustomer.Email;
                customer.Address = currentCustomer.Address;
                customer.Phone = currentCustomer.Phone;
                customer.UserName = currentCustomer.UserName;

                if (ModelState.IsValid)
                {
                    customerData.UpdateCustomer(newCustomer);
                    TempData["msg"] = "Profile Updated!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Failed to update profile!";
                    return RedirectToAction("Index");
                }
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
            if (!customerData.GetCustomerById((int)Session["user_id"]).Password.Equals(collection["currentPassword"]))
            {
                TempData["msg"] = "Invalid current password!";
                return RedirectToAction("ChangePassword", "CustomerProfile", id);
            }
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
        public ActionResult PurchaseHistory(int id)
        {
            ViewBag.PurchaseHistory = salesLogData.GetPurchaseHistoryByCustomerId(id);
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
            if (image==null)
            {
                TempData["msg"] = "Please select a picture first!";
                return RedirectToAction("Index");
            }
            else if (image.ContentLength>0)
            {
                Models.Customer customer =customerData.GetCustomerByEmail((string)Session["user_email"]);
                if (customer!=null)
                {
                    string ImageFileName = Path.GetFileName(image.FileName);
                    string ImageExtension = Path.GetExtension(image.FileName);
                    if (ImageExtension.Equals(".jpg") || ImageExtension==".jpg" || ImageExtension.Equals(".JPG") || ImageExtension == ".JPG" || ImageExtension.Equals(".PNG") || ImageExtension == ".PNG" || ImageExtension.Equals(".png") || ImageExtension == ".png")
                    {
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
                        TempData["msg"] = "Please choose JPG or PNG files only!";
                        return RedirectToAction("Index");
                    }
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
                if (customer.ProfilePic==null)
                {
                    TempData["msg"] = "No profile picture uploaded to remove!";
                    return RedirectToAction("Index");
                }
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
        [HttpGet]
        public ActionResult PaymentAndShipping()
        {
            ViewBag.PaymentAndShippingData = ordersData.GetPaymentAndShippingDataByCustomerId((int)Session["user_id"]);
            if (ViewBag.PaymentAndShippingData==null)
            {
                TempData["msg"]= "Could not found your payment and shipping data!";
                return RedirectToAction("Index");
            }
            return View(customerData.GetCustomerByEmail((string)Session["user_email"]));
        }
        [HttpPost]
        public ActionResult PaymentAndShipping(Models.OrderData order)
        {
            order.Id = ordersData.GetOrderIdByCustomerId((int)Session["user_id"]);
            order.Customer_Id = (int)Session["user_id"];

            if (ModelState.IsValid)
            {
                ordersData.UpdatePaymentAndShippingInfo(order);
                TempData["msg"] = "Updated payment and shipping details successfully!";
                return RedirectToAction("PaymentAndShipping");
            }
            else
            {
                TempData["msg"] = "Failed to update payment and shipping details!\nFill up the form carefully!";
                return RedirectToAction("PaymentAndShipping");
            }

        }
        [HttpGet]
        public ActionResult GetPurchaseCategoryData()
        {
            var list = salesLogData.GetPurchaseVisualizationData((int)Session["user_id"], 2020,2021);
            return Json(new JavaScriptSerializer().Serialize(list) ,JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult CancelOrder(int id)
        //{
        //    if (salesLogData.CancelOrderByOrderId(id) == true)
        //    {
        //        TempData["msg"] = "Cancelled the order!";
        //    }
        //    else
        //    {
        //        TempData["msg"] = "Failed to cancel the order!";
        //    }
        //    return Redirect("/CustomerProfile/PurchaseHistory/" + (int)Session["user_id"]);
        //}
    }
}