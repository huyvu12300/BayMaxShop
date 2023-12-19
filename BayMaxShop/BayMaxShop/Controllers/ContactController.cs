using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact model)
        {
            if (ModelState.IsValid) 
            {
                model.CreatedDate = DateTime.Now;
                db.Contacts.Add(model);
                db.SaveChanges();
            }
            return View(model);
        }
    }
}