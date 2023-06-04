using BayMaxShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using BayMaxShop.Models.EF;

namespace BayMaxShop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(string Searchtext, int? page)
        {
            

            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }




        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }


        public ActionResult ProductCategory(string alias, int? id, string Searchtext, int? page)
        {
          
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId() 
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive == true).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive == true).Take(12).ToList();
            return PartialView(items);
        }
    }
}