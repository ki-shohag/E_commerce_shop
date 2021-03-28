using OnlineTechShop.Models;
using OnlineTechShop.Models.AdminModel.AdminDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult UpdateProfile(int id, Models.Admin admin, HttpPostedFileBase image)
        {
            string im = "";
            if (image == null)
            {
                im = admin.ProfilePic;
            }
            else
            {
                im = AdminProfileImg(image);
            }
            admin.Id = id;
            admin.ProfilePic = im;
            admin.LastUpdated = DateTime.Now;
            context.Entry(admin).State = EntityState.Modified;
            return RedirectToAction("Index","AdminProfile");
        }

        public string AdminProfileImg(HttpPostedFileBase image)
        {
            string img;

            img = Path.Combine(Server.MapPath("~/Contents/Upload_Files/Admins/" + Path.GetFileName(image.FileName)));

            image.SaveAs(img);

            img = Path.GetFileName(image.FileName);

            return img;
        }
    }
}