using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var items = db.Brands;
            return View(items);
        }

        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Brand model)
        {
            if(ModelState.IsValid) 
            {
                model.CreatedDate = DateTime.Now;
                model.Alias = BayMaxShop.Models.Commons.Filter.FilterChar(model.BrandName);
                db.Brands.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id) 
        {
            var items = db.Brands.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand model) 
        {
            if(ModelState.IsValid) 
            {
                model.CreatedDate = DateTime.Now;
                model.Alias = BayMaxShop.Models.Commons.Filter.FilterChar(model.BrandName);
                db.Brands.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Brands.Find(id);
            if (item != null)
            {
                db.Brands.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}