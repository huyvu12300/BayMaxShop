using BayMaxShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop() 
        {
            var items = db.Menus.OrderByDescending(x => x.Position).ToList();
            return PartialView("MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("MenuProductCategory", items);
        }
        public ActionResult MenuArrival() 
        { 
            var items = db.ProductCategories.ToList();
            return PartialView("MenuArrival",items); 
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("MenuLeft", items);
        }
        public ActionResult MenuBrand(int? id) 
        {
            if (id != null) 
            {
                ViewBag.BrId = id;
            }
            var items = db.Brands.ToList();
            return PartialView("MenuBrand", items);
        }
    }
}