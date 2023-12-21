using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Booking
        public ActionResult Index(string Searchtext, int? page)
        {
            var items = db.AddressBooks.OrderByDescending(x => x.Id).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult View(int id)
        {
            var items = db.AddressBooks.Find(id);
            return View(items);
        }
    }
}