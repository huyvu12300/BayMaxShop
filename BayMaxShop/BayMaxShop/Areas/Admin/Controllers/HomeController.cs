using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin, Staff")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}