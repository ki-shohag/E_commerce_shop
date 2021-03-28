using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTechShop.Models;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;
using Rotativa;

namespace OnlineTechShop.Controllers.BuyingAgent
{
    public class BuyingAgentProfileController : Controller
    {
        BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
        OldProductDataModel oldProductData = new OldProductDataModel();

        public ActionResult Index()
        {
            return View(buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]));
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]));
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
            Models.BuyingAgent buyingAgent = buyingAgentData.GetBuyingAgentById(id);

            if (collection["currPassword"] == null)
            {
                TempData["msg"] = "Current Password field is necessary";
                return RedirectToAction("Edit", "BuyingAgentProfile", id);
            }
            else if (!buyingAgent.Password.Equals(collection["currPassword"]))
            {
                TempData["msg"] = "Cuurent password did not match!";
                return RedirectToAction("Edit", "BuyingAgentProfile", id);
            }
            else if (buyingAgentData.GetBuyingAgentByEmail((string)buyingAgent.Email) != null && buyingAgent.Email != (string)Session["buyingAgent_email"] && !buyingAgent.Email.Equals((string)Session["buyingAgent_email"]))
            {

                TempData["msg"] = "Email address already registered!";
                return RedirectToAction("Edit", "BuyingAgentProfile", id);
            }
            else if (buyingAgentData.GetBuyingAgentByUserName((string)buyingAgent.UserName) != null && buyingAgent.UserName != (string)Session["buyingAgent_name"] && !buyingAgent.UserName.Equals((string)Session["buyingAgent_name"]))
            {
                TempData["msg"] = "User Name already registered!";
                return RedirectToAction("Edit", "BuyingAgentProfile", id);
            }
            else
            {
                Models.BuyingAgent newBuyingAgent = buyingAgentData.GetBuyingAgentById(id);
                newBuyingAgent.FullName = buyingAgent.FullName;
                newBuyingAgent.Address = buyingAgent.Address;
                newBuyingAgent.Phone = buyingAgent.Phone;
                newBuyingAgent.Email = buyingAgent.Email;
                buyingAgentData.UpdateBuyingAgent(newBuyingAgent);
                TempData["msg"] = "Profile Updated!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            return View(buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]));
        }
        [HttpPost]
        public ActionResult ChangePassword(int id, FormCollection collection)
        {
            BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
            Models.BuyingAgent buyingAgent = buyingAgentData.GetBuyingAgentById(id);

            if (!buyingAgent.Password.Equals(collection["oldPassword"]))
            {
                TempData["msg"] = "Old password did not match!";
                return RedirectToAction("ChangePassword", "BuyingAgentProfile", id);
            }
            else if (!collection["newPassword"].Equals(collection["confirmNewPassword"]))
            {
                TempData["msg"] = "Passwords did not match!";
                return RedirectToAction("ChangePassword", "BuyingAgentProfile", id);
            }
            else if (collection["newPassword"] == null)
            {
                TempData["msg"] = "New password cannot be empty!";
                return RedirectToAction("ChangePassword", "BuyingAgentProfile", id);
            }
            else if (collection["confirmNewPassword"] == null)
            {
                TempData["msg"] = "Confirm password cannot be empty!";
                return RedirectToAction("ChangePassword", "BuyingAgentProfile", id);
            }
            else if (collection["oldPassword"] == null)
            {
                TempData["msg"] = "Old password cannot be emoty";
                return RedirectToAction("ChangePassword", "BuyingAgentProfile", id);
            }
            else
            {
                buyingAgent.Password = collection["newPassword"];
                buyingAgentData.UpdateBuyingAgent(buyingAgent);
                TempData["msg"] = "Password changed successfully!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult BuyingHistory(int? id)
        {
            ViewBag.Data = buyingAgentData.GetAllBuyingData().ToList();
            return View(buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]));
        }

        [HttpGet]
        public ActionResult ExportHelper()
        {
            ViewBag.ExportData = buyingAgentData.GetAllBuyingData().ToList();
            return View();
        }

        [HttpGet]
        public ActionResult ExportPdf()
        {
            var report = new ActionAsPdf("ExportHelper");
            return report;
        }


        [HttpPost]
        public ActionResult UploadProfilePic(HttpPostedFileBase image)
        {
            if (image.ContentLength > 0)
            {
                Models.BuyingAgent buyingAgent = buyingAgentData.GetBuyingAgentByEmail((string)Session["buyingAgent_email"]);
                if (buyingAgent != null)
                {
                    string ImageFileName = Path.GetFileName(image.FileName);
                    string ImageExtension = Path.GetExtension(image.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/Contents/Upload_Files/Buying_Agent/ProfilePic"), ImageFileName.Replace(ImageFileName, Session["buyingAgent_email"].ToString() + ImageExtension));
                    image.SaveAs(FolderPath);
                    buyingAgent.ProfilePic = ImageFileName.Replace(ImageFileName, Session["buyingAgent_email"].ToString() + ImageExtension);
                    Session["buyingAgent_profile_pic"] = buyingAgent.ProfilePic;
                    buyingAgentData.UpdateBuyingAgent(buyingAgent);
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
    }
}