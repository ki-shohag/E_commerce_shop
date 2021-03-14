using OnlineTechShop.Models;
using OnlineTechShop.Models.AdminModel.AdminDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class AdminSettingsController : Controller
    {
        TechShopDbEntities context = new TechShopDbEntities();
        AdDataModel data = new AdDataModel();

        [HttpGet]
        public ActionResult Index()
        {
            var admin = data.GetAdminByEmail(Session["email"].ToString());
            return View(admin);
        }

        [HttpPost]
        public ActionResult UpdateProfile(FormCollection collection, HttpPostedFileBase image)
        {
           var admin = context.Admins.Find(Convert.ToInt32(collection["id"]));

            string img = "";
            if (image == null)
            {
                img = admin.ProfilePic;
            }

            else
            {
                img = AdminProfileImg(image);
            }

            admin.Id = Convert.ToInt32(collection["id"]);
            admin.Phone = collection["phone"];
            admin.Address = collection["address"];
            admin.ProfilePic = img;
            admin.LastUpdated = DateTime.Now;
            context.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index", "AdminProfile");
        }

        public string AdminProfileImg(HttpPostedFileBase image)
        {
            string img;

            img = Path.Combine(Server.MapPath("~/Contents/Upload_Files/Admins/" + Path.GetFileName(image.FileName)));

            image.SaveAs(img);

            img = Path.GetFileName(image.FileName);

            return img;
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var admin = data.GetAdminByEmail(Session["email"].ToString());
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            //return Content(collection["password"] + " " + collection["newPassword"] + " " + collection["confirmPassword"]);
            var admin = data.CheckAccountPassword(collection["password"], Session["email"].ToString());

            if (admin == null)
            {
                TempData["msg1"] = "Incorrect password!!";
                return View();
            }

            else
            {
                if (collection["newPassword"].Equals(collection["confirmPassword"]))
                {
                    admin.Password = collection["newPassword"];
                    admin.LastUpdated = DateTime.Now;
                    context.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index", "AdminProfile");
                    

                }
                else
                {
                    TempData["msg2"] = "Passwords did not match!!";
                    return View();
                }
            }
        }
    }
}
