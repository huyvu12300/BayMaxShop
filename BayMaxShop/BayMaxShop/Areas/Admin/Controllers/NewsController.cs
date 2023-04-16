using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = db.News.OrderByDescending(x => x.Title).ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model) 
        {
            if(ModelState.IsValid)
            {
                model.CategoryId = 3;
                model.CreatedDate= DateTime.Now;
                model.ModifiedDate= DateTime.Now;
                model.Alias = BayMaxShop.Models.Commons.Filter.FilterChar(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}