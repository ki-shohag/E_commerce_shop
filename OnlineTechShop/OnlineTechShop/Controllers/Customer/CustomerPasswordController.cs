using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using OnlineTechShop.Models.CustomerAccess.DataModels;
namespace OnlineTechShop.Controllers.Customer
{
    public class CustomerPasswordController : Controller
    {
        // GET: CustomerPassword
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            CustomerDataModel customerData = new CustomerDataModel();
            Models.Customer customer = customerData.GetCustomerByEmail(collection["email"]);
            if (customer != null)
            {
                if ((string)customer.Status == "Active")
                {
                    Random randomNumber = new Random();
                    Session["verification_code"] = randomNumber.Next(1000, 9999).ToString();
                    Session["user_email"] = customer.Email;
                    //Send email
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Verification Code", "shohag.cse45@gmail.com"));
                    message.To.Add(new MailboxAddress("Customer", "koushikur.aiub@gmail.com"));
                    message.Subject = "Password Reset";
                    message.Body = new TextPart("plain")
                    {
                        Text = "Verification code is : " + Session["verification_code"]
                    };
                    using (var client  = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("shohag.cse45@gmail.com", "cinecarnival");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return RedirectToAction("VerifyCode");
                }
                else
                {
                    TempData["msg"] = "Sorry, you are blocked from the system!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["msg"] = "The email is not registered!\nPlease try again!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult VerifyCode()
        {
            if (Session["user_email"]==null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult VerifyCode(FormCollection collection)
        {
            string verificationCode = collection["verificationCode"];
            if (verificationCode.Equals((string)Session["verification_code"]) || verificationCode==(string)Session["verification_code"])
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                TempData["msg"] = "Verification code did not match!";
                return RedirectToAction("VerifyCode");
            }

        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(FormCollection collection)
        {
            if (!collection["newPassword"].Equals(collection["confirmNewPassword"]))
            {
                TempData["msg"] = "Passwords did not match!";
                return RedirectToAction("ResetPassword");
            }
            else if (collection["newPassword"]==null)
            {
                TempData["msg"] = "New password cannot be empty!";
                return RedirectToAction("ResetPassword");
            }
            else if (collection["confirmNewPassword"] == null)
            {
                TempData["msg"] = "Confirm password cannot be empty!";
                return RedirectToAction("ResetPassword");
            }
            else
            {
                CustomerDataModel customerData = new CustomerDataModel();
                Models.Customer customer = customerData.GetCustomerByEmail((string)Session["user_email"]);
                customer.Password = collection["newPassword"];
                customerData.UpdateCustomer(customer);
                Session.Clear();
                TempData["msg"] = "Password reset was successfull!\nWant to login?";
                return RedirectToAction("Index", "CustomerLogin");
            }
        }
    }
}