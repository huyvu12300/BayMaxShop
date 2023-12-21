using BayMaxShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Staff")]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                // Người dùng là Customer, chuyển hướng đến trang customer
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.Orders = db.Orders.Count();
            ViewBag.Products = db.Products.Count();
            ViewBag.News = db.News.Count();
            ViewBag.Account = db.Users.Count();
            return View();
        }
    }
}