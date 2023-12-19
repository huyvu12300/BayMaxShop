using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DsContact()
        {
            try
            {
                var contact = db.Contacts.OrderByDescending(x => x.CreatedDate).Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    email = x.Email,
                    message = x.Message
                })
                .ToList();
                return Json(new { code = 200, dsContact = contact, msg = "Successfully get list contact!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Get list contact false: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        public JsonResult Reply(int id)
        {
            try
            {
                var detail = db.Contacts.Where(i => i.Id == id).Select(i => new
                {
                    id = i.Id,
                    name = i.Name,
                    email = i.Email
                })
                .SingleOrDefault();
                return Json(new { code = 200, detail = detail, msg = "Successfully get detail!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Failed get detail!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendMail(string name, string email, string subject)
        {
            var result = BayMaxShop.Common.Common.SendMail(name, subject, "<p>Xin chào " + name + ",<br /> " + subject + " <br />Cảm ơn bạn liên hệ với chúng tôi.</p>", email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}