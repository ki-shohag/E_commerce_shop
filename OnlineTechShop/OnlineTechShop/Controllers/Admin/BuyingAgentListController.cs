using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class BuyingAgentListController : Controller
    {
        TechShopDbEntities context = new TechShopDbEntities();
        // GET: BuyingAgentList
        public ActionResult Index()
        {
            return View(context.BuyingAgents.ToList());
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
            buyingAgent.Id = id;
            buyingAgent.LastUpdated = DateTime.Now;
            context.Entry(buyingAgent).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
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