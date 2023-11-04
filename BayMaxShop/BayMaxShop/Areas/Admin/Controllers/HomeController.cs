using BayMaxShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.Orders = db.Orders.Count();
            ViewBag.Products = db.Products.Count();
            ViewBag.Posts = db.Posts.Count();
            ViewBag.News = db.News.Count();

            return View();
        }
    }
}