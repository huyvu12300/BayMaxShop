using BayMaxShop.Models;
using BayMaxShop.Models.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayMaxShop.Controllers
{

    class GetInfo
    {
        public static string Email;
    }
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
            string id = User.Identity.GetUserId();
            var listAddress = db.AddressBooks.Where(x => x.UserID.Contains(id));
            ViewBag.InfoOrder = listAddress;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            string id = User.Identity.GetUserId();
            var listAddress = db.AddressBooks.Where(x => x.UserID.Contains(id));
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    var order = new BayMaxShop.Models.EF.Order();
                    if (id!= null)
                    {
                       foreach (var item in listAddress)
                       {
                            if (item.IsDefault)
                            {
                                order.CustomerName = item.CustomerName;
                                order.Phone = item.Phone;
                                order.Address = item.Address;
                                order.Email = item.Email;
                            }
                       }    
                    }   
                    else
                    {
                        order.CustomerName = req.CustomerName;
                        order.Phone = req.Phone;
                        order.Address = req.Address;
                        order.Email= req.Email;                 
                    }    

                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                    }));
                    order.TotalAmount = cart.Items.Sum(x=>(x.Price*x.Quantity));
                    if (User.Identity.GetUserId() == null)
                    {
                        order.IdUser = null;
                    }
                    else
                    {
                        order.IdUser = User.Identity.GetUserId();
                    }
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate= DateTime.Now;
                    order.ModifiedDate= DateTime.Now;
                    order.CreatedBy = req.CustomerName;
                    order.Email= req.Email;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    SendMail(GetInfo.Email,cart, order);
                    return RedirectToAction("CheckOutSuccess");
                }
            return Json(code);
        }

        private void SendMail(string email, ShoppingCart cart, Models.EF.Order order)
        {
            List<Product> products = db.Products.ToList();
            List<OrderDetail> orderDetails = order.OrderDetails.ToList();
            foreach (var itemProduct in products)
            {
                foreach (var itemOrderDetail in orderDetails)
                {
                    if (itemProduct.Id == itemOrderDetail.ProductId)
                    {
                        itemProduct.Quantity -= itemOrderDetail.Quantity;
                        db.Products.Attach(itemProduct);
                        db.Entry(itemProduct).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            db.Orders.Add(order);
            db.SaveChanges();
            var strSanPham = "";
            var thanhtien = decimal.Zero;
            var phiVanChuyen = decimal.Zero;
            var TongTien = decimal.Zero;
            foreach (var sp in cart.Items)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + sp.ProductName + "</td>";
                strSanPham += "<td>" + sp.Quantity + "</td>";
                strSanPham += "<td>" + BayMaxShop.Common.Common.FormatNumber(sp.TotalPrice) + "</td>";
                strSanPham += "</tr>";
                thanhtien += sp.Quantity * sp.Price;
            }
            TongTien = thanhtien + phiVanChuyen;
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
            contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
            contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", "Tên Người Nhận: " + order.CustomerName);
            contentCustomer = contentCustomer.Replace("{{Phone}}", "Số Điện Thoại: " + order.Phone);
            contentCustomer = contentCustomer.Replace("{{Email}}", "Email: " + email);
            contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", "Địa Chỉ Nhận Hàng: " + order.Address);
            switch (order.TypePayment)
            {
                case 1:
                    contentCustomer = contentCustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: COD");
                    break;
                case 2:
                    contentCustomer = contentCustomer.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: Chuyển Khoản");
                    break;
                default:
                    break;
            }
            contentCustomer = contentCustomer.Replace("{{ThanhTien}}", BayMaxShop.Common.Common.FormatNumber(thanhtien, 0));
            contentCustomer = contentCustomer.Replace("{{PhiVanChuyen}}", BayMaxShop.Common.Common.FormatNumber(phiVanChuyen, 0));
            contentCustomer = contentCustomer.Replace("{{TongTien}}", BayMaxShop.Common.Common.FormatNumber(TongTien, 0));
            BayMaxShop.Common.Common.SendMail("BayMaxShop", "Đơn hàng #" + order.Code, contentCustomer.ToString(), email);

            string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
            contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
            contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
            contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", "Tên Người Nhận: " + order.CustomerName);
            contentAdmin = contentAdmin.Replace("{{Phone}}", "Số Điện Thoại: " + order.Phone);
            contentAdmin = contentAdmin.Replace("{{Email}}", "Emaili: " + email);
            contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", "Địa Chỉ Nhận Hàng: " + order.Address);
            switch (order.TypePayment)
            {
                case 1:
                    contentAdmin = contentAdmin.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: COD");
                    break;
                case 2:
                    contentAdmin = contentAdmin.Replace("{{HinhThucThanhToan}}", "Hình Thức Thanh Toán: Chuyển Khoản");
                    break;
                default:
                    break;
            }
            contentAdmin = contentAdmin.Replace("{{ThanhTien}}", BayMaxShop.Common.Common.FormatNumber(thanhtien, 0));
            contentAdmin = contentAdmin.Replace("{{PhiVanChuyen}}", BayMaxShop.Common.Common.FormatNumber(phiVanChuyen, 0));
            contentAdmin = contentAdmin.Replace("{{TongTien}}", BayMaxShop.Common.Common.FormatNumber(TongTien, 0));
            BayMaxShop.Common.Common.SendMail("BayMaxShop", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
            cart.ClearCart();
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);

            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.ProductName,
                    CategoryName = checkProduct.ProductCategory.ProductCategoryName,
                    Alias = checkProduct.Alias,
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
                if (cart.CheckQuantityAddtoCart(checkProduct.Quantity, item, quantity))
                {
                    cart.AddToCart(item, quantity);
                    Session["Cart"] = cart;
                    code = new { Success = true, msg = "Thêm sản phẩm thành công!", code = 1, Count = cart.Items.Count };
                }
                else
                {
                    code = new { Success = false, msg = "", code = -1, Count = 0 };
                }

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