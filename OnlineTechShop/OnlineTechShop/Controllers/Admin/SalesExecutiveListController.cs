using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class SalesExecutiveListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        public ActionResult Index(string name)
        {
            if (name == null)
            {
                return View(context.SalesExecutives.ToList());
            }
            else
            {
                var salesExecutives = context.SalesExecutives.Where(p => p.FullName.Contains(name)).ToList();
                return View(salesExecutives);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.SalesExecutive salesExecutive)
        {
            if (ModelState.IsValid)
            {
                salesExecutive.ProfilePic = "default.jpg";
                salesExecutive.JoiningDate = DateTime.Now;
                salesExecutive.LastUpdated = DateTime.Now;
                context.SalesExecutives.Add(salesExecutive);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var salesExecutive = context.SalesExecutives.Find(id);
            return View(salesExecutive);
        }

        [HttpPost]
        public ActionResult Edit(int id, Models.SalesExecutive salesExecutive)
        {
            salesExecutive.Id = id;
            salesExecutive.LastUpdated = DateTime.Now;
            context.Entry(salesExecutive).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var removeSalesExecutive = context.SalesExecutives.Find(id);
            return View(removeSalesExecutive);
        }

        [HttpPost]
        public ActionResult Delete(int id, Models.SalesExecutive salesExecutive)
        {
            context.SalesExecutives.Remove(context.SalesExecutives.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}