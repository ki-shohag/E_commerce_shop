using OnlineTechShop.Models;
using OnlineTechShop.Models.AdminModel.AdminDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class AdminListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        // GET: AdminList
        public ActionResult Index(string name)
        {
            if (name == null)
            {
                return View(context.Admins.ToList());
            }
            else
            {
                var admin = context.Admins.Where(p => p.FullName.Contains(name)).ToList();
                return View(admin);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.ProfilePic = "default.jpg";
                admin.JoiningDate = DateTime.Now;
                admin.LastUpdated = DateTime.Now;
                context.Admins.Add(admin);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var removeAdmin = context.Admins.Find(id);
            return View(removeAdmin);
        }

        [HttpPost]
        public ActionResult Delete(int id, Models.Admin admin)
        {
            context.Admins.Remove(context.Admins.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}