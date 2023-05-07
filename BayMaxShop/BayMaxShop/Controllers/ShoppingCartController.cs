using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }


        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart=cart;
            }
            return View();
        }

        public ActionResult CheckOutSuccess()
        {
            return View();
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }


            return PartialView();
        }

        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }


            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }    
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
         
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if(ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    Order order = new Order();
                    order.CustomerName= req.CustomerName;
                    order.Phone= req.Phone;
                    order.Address= req.Address;
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                    }));
                    order.TotalAmount = cart.Items.Sum(x=>(x.Price*x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate= DateTime.Now;
                    order.ModifiedDate= DateTime.Now;
                    order.CreatedBy = req.CustomerName;
                    order.Email= req.Email;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach(var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+sp.ProductName+"</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + BayMaxShop.Common.Common.FormatNumber(sp.TotalPrice,0) + "</td>";
                        strSanPham += "</tr>"; 
                        thanhtien = sp.Price * sp.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentcustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentcustomer = contentcustomer.Replace("{{MaDon}}",order.Code);
                    contentcustomer = contentcustomer.Replace("{{SanPham}}", strSanPham);
                    contentcustomer = contentcustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentcustomer = contentcustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentcustomer = contentcustomer.Replace("{{Phone}}", order.Phone);
                    contentcustomer = contentcustomer.Replace("{{Email}}", req.Email);
                    contentcustomer = contentcustomer.Replace("{{DiaChiNhanHang}}", "Địa Chỉ Nhận Hàng: " + order.Address);
                    if (order.TypePayment == 1)
                    {
                        contentcustomer = contentcustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: COD");
                    }
                    if (order.TypePayment == 2)
                    {
                        contentcustomer = contentcustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: Chuyển Khoản");
                    }
                    contentcustomer = contentcustomer.Replace("{{ThanhTien}}", BayMaxShop.Common.Common.FormatNumber(thanhtien,0));
                    contentcustomer = contentcustomer.Replace("{{TongTien}}", BayMaxShop.Common.Common.FormatNumber(TongTien,0));
                    BayMaxShop.Common.Common.SendMail("BayMaxShop","Đơn hàng #" + order.Code,contentcustomer.ToString(),req.Email);
                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", BayMaxShop.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", BayMaxShop.Common.Common.FormatNumber(TongTien, 0));
                    BayMaxShop.Common.Common.SendMail("BayMaxShop", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    if (order.TypePayment == 1)
                    {
                        contentcustomer = contentcustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: COD");
                    }
                    if (order.TypePayment == 2)
                    {
                        contentcustomer = contentcustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: Chuyển Khoản");
                    }
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = ( ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }



                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias= checkProduct.Alias,
                    Quantity = quantity
                };



                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;

                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };

               
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete (int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart!= null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = -1, Count = 0 };

                }    
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart!= null)
            {
                cart.ClearCart();
                return Json(new { Success = true});
            }
            return Json(new { Success = false });
        }


    }

}