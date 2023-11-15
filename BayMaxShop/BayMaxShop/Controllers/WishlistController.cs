using BayMaxShop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Controllers
{
    public class WishlistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Wishlist
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var items = db.Wishlists.Where( x => x.UserID.Contains(id));
            return View(items);
        }
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}