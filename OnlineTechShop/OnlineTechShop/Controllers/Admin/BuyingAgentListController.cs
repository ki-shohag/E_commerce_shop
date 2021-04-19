using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class BuyingAgentListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        public ActionResult Index(string name)
        {
            if (name == null)
            {
                return View(context.BuyingAgents.ToList());
            }
            else
            {
                var buyingAgents = context.BuyingAgents.Where(p => p.FullName.Contains(name)).ToList();
                return View(buyingAgents);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.BuyingAgent buyingAgent)
        {
            if (ModelState.IsValid)
            {
                buyingAgent.ProfilePic = "default.jpg";
                buyingAgent.JoiningDate = DateTime.Now;
                buyingAgent.LastUpdated = DateTime.Now;
                context.BuyingAgents.Add(buyingAgent);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var buyingAgent = context.BuyingAgents.Find(id);
            return View(buyingAgent);
        }

        [HttpPost]
        public ActionResult Edit(int id, Models.BuyingAgent buyingAgent)
        {
            if (ModelState.IsValid)
            {
                buyingAgent.Id = id;
                buyingAgent.LastUpdated = DateTime.Now;
                context.Entry(buyingAgent).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(context.BuyingAgents.Find(id));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var removeBuyingAgent = context.BuyingAgents.Find(id);
            return View(removeBuyingAgent);
        }

        [HttpPost]
        public ActionResult Delete(int id, Models.BuyingAgent buyingAgent)
        {
            context.BuyingAgents.Remove(context.BuyingAgents.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}