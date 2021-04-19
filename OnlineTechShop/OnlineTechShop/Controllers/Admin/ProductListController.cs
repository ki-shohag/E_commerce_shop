using OnlineTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace OnlineTechShop.Controllers.Admin
{
    public class ProductListController : AdminBaseController
    {
        TechShopDbEntities context = new TechShopDbEntities();
        
        public ActionResult Index(string name)
        {
            if (name == null)
            {
                return View(context.Products.ToList());
            }
            else
            {
                var products = context.Products.Where(p => p.ProductName.Contains(name)).ToList();
                return View(products);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string img = ProductImage(image);
                product.Images = img;
                product.DateAdded = DateTime.Now;
                product.LastUpdated = DateTime.Now;
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public string ProductImage(HttpPostedFileBase image)
        {
            string img = "";
            img = Path.Combine(Server.MapPath("~/Contents/Upload_Files/Products/" + Path.GetFileName(image.FileName)));

            image.SaveAs(img);

            img = Path.GetFileName(image.FileName);

            return img;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productToEdit = context.Products.Find(id);
            return View(productToEdit);
        }


        [HttpPost]
        public ActionResult Edit(int id, Models.Product product, HttpPostedFileBase image)
        {
            string im ="";
            if (image == null)
            {
                im = product.Images;
            }

            else
            {
                im = ProductImage(image);
            }

            if (ModelState.IsValid)
            {
                product.Id = id;
                product.Images = im;
                product.LastUpdated = DateTime.Now;
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(context.Products.Find(id));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var removeProduct = context.Products.Find(id);
            return View(removeProduct);
        }
        [HttpPost]
        public ActionResult Delete(int id, Models.Product product)
        {
            context.Products.Remove(context.Products.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(context.Products.ToList());
        }

        [HttpGet]
        public ActionResult AddQuantity(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddQuantity(FormCollection collection)
        {
            var product = context.Products.Find(Convert.ToInt32(collection["proId"]));
            product.Quantity = Convert.ToInt32(product.Quantity) + Convert.ToInt32(collection["newStock"]);
            product.LastUpdated = DateTime.Now;
            context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}