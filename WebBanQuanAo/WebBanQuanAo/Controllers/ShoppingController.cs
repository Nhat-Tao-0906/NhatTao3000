using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Class;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;
using System.Configuration;
using PayPal.Api;
using System.IO;

namespace WebBanQuanAo.Controllers
{
    public class ShoppingController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        //private readonly string clientId = ConfigurationManager.AppSettings["clientId"].ToString();
        //private readonly string clientSecret = ConfigurationManager.AppSettings["clientSecret"].ToString();


        
        // GET: Shopping
        public ActionResult ThemGioHang(FormCollection form)
        {
            int id = int.Parse(Session["IDPro"].ToString());
            if (Session["giohang"] == null)
            {
                GioHang gh = new GioHang();
                var sp = db.products.Where(m => m.IDProduct == id).Single();
                gh.ID = sp.IDProduct;
                gh.Ten = sp.NameProduct;
                gh.Gia = sp.Price;
                gh.HinhAnh = sp.Image1;
                gh.SoLuong = 1;
                gh.Size = form["size"];
                List<GioHang> listgiohang = new List<GioHang>();
                listgiohang.Add(gh);
                Session["giohang"] = listgiohang;
            }
            else
            {
                List<GioHang> listgiohang = (List<GioHang>)Session["giohang"];
                var test = listgiohang.Find(m => m.ID == id && m.Size == form["size"]);
                if (test != null)
                {
                    test.SoLuong++;
                }
                else
                {
                    GioHang gh = new GioHang();
                    var sp = db.products.Where(m => m.IDProduct == id).Single();
                    gh.ID = sp.IDProduct;
                    gh.Ten = sp.NameProduct;
                    gh.Gia = sp.Price;
                    gh.HinhAnh = sp.Image1;
                    gh.SoLuong = 1;
                    gh.Size = form["size"];
                    listgiohang.Add(gh);
                    Session["giohang"] = listgiohang;
                }
            }
            return RedirectToAction("Home","Home");
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public PartialViewResult BagCart()
        {
            int soluong = 0;
            List <GioHang> listgh = (List<GioHang>)Session["giohang"];
            if (listgh != null)
                soluong = listgh.Count();
            ViewBag.soluong = soluong;
            return PartialView("BagCart");
        }
        public ActionResult XoaSanPham(int id)
        {
            List<GioHang> listgh = (List<GioHang>)Session["giohang"];
            var sp = listgh.Find(m => m.ID == id);
            listgh.Remove(sp);
            return RedirectToAction("GioHang", "Shopping");
        }
        public ActionResult CapNhatSanPham(GioHang g)
        {
            List<GioHang> listgiohang = (List<GioHang>)Session["giohang"];
            var sp = listgiohang.Find(m => m.ID == g.ID && m.Size == g.Size);
            foreach (var item in listgiohang)
            {
                if (sp.ID == item.ID && sp.Size == item.Size)
                {
                    item.SoLuong = g.SoLuong;
                }
            }
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult ThanhToan()
        {
            if (Session["idcusReview"] != null)
            {
                int id = int.Parse(Session["idcusReview"].ToString());
                var cus = db.customer.Where(m => m.IDCus == id).FirstOrDefault();
                return View(cus);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToan(FormCollection form)
        {
            if (form["HinhThuc"] == "1")
            {
                string email = form["email"];
                var cus = db.customer.Where(m => m.Email == email).FirstOrDefault();
                if (cus != null && cus.Status == true || cus == null)
                {
                    Register reg = new Register();
                    reg.Name = form["Name"];
                    reg.Email = form["Email"];
                    reg.Phone = form["Phone"];
                    reg.Address = form["Address"];
                    Session["infocus"] = reg;
                    return RedirectToAction("PayMent");
                }
                else
                {
                    ViewBag.errorbanner = "Bạn đã bị cấm mua hàng tại web";
                    return View();
                }
            }
            try
            {
                Customer c = new Customer();
                int i = 0;
                foreach(var item in db.customer)
                {
                    if (item.Email == form["Email"])
                    {
                        i = 1;
                        break;
                    }
                }
                if (i != 1)
                {
                    c.Name = form["Name"];
                    c.Image = "anh-dai-dien-FB-200.jpg";
                    c.Email = form["Email"];
                    c.Phone = form["Phone"];
                    c.Address = form["Address"];
                    c.Status = true;
                    db.customer.Add(c);
                    db.SaveChanges();
                }
                else
                {
                    string Email = form["Email"];
                    c = db.customer.Where(m => m.Email == Email).FirstOrDefault();
                    if (c.Status == false)
                    {
                        return View();
                    }
                }        
                Bill b = new Bill();
                b.IDCus = c.IDCus;
                b.DateCreated = DateTime.Now;
                b.ShippingCost = 25;
                b.BillTotal = 0;
                b.PayMentStatus = 0;
                b.OrderStatus = 1;
                db.Bill.Add(b);
                List<GioHang> lgh = (List<GioHang>)Session["giohang"];
                double total = 0;
                int quanitysold = 0;
                Product product = new Product();
                foreach (var item in lgh)
                {
                    BillDetails bd = new BillDetails();
                    bd.IDBill = b.IDBill;
                    double tongtien = 0;
                    bd.IDProduct = item.ID;
                    bd.Quantity = item.SoLuong;
                    bd.UnitPrice = item.Gia;
                    bd.Size = item.Size;
                    tongtien += (item.Gia * item.SoLuong);
                    db.billDetails.Add(bd);
                    total += tongtien;
                    quanitysold += item.SoLuong;
                    product = db.products.Where(m => m.IDProduct == item.ID).FirstOrDefault();
                    product.QuantitySold += item.SoLuong;
                    product.QuantityRemaining -= item.SoLuong;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    var size = db.size_Products.Where(m => m.IDPro == item.ID && m.size == item.Size).FirstOrDefault();
                    size.Quantity -= item.SoLuong;
                    db.Entry(size).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                total += 25;
                b.BillTotal = total;
                db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["madonhang"] = b.IDBill;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Template/newoder.html"));
                content = content.Replace("{{NameProduct}}", lgh[0].Ten);
                content = content.Replace("{{Quantity}}", lgh[0].SoLuong.ToString());
                content = content.Replace("{{Price}}", lgh[0].Gia.ToString());


                content = content.Replace("{{HinhAnh}}", lgh[0].HinhAnh);
                content = content.Replace("{{Name}}", c.Name);
                content = content.Replace("{{Email}}", c.Email);
                content = content.Replace("{{Phone}}", c.Phone);
                content = content.Replace("{{Address}}", c.Address);
                content = content.Replace("{{Total}}", total.ToString());

                new MailHelper().SenMail(c.Email, "Đơn hàng mới", content);
                return RedirectToAction("CheckoutSuccess");
            }
            catch
            {
                if (form["Name"] == "" || form["Name"] == null)
                    ViewBag.errorName = "Họ tên không được bỏ trống";
                if (form["Email"] == "" || form["Email"] == null)
                    ViewBag.errorEmail = "Email không được bỏ trống";
                if (form["Phone"] == "" || form["Phone"] == null)
                    ViewBag.errorPhone = "SĐT không được bỏ trống";
                if (form["Address"] == "" || form["Address"] == null)
                    ViewBag.errorAddress = "Địa chỉ không được bỏ trống";
                return View();
            }

        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var paypaloder = DateTime.Now.Ticks;
            double tongtien = 0;
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            foreach (var item in (List<GioHang>)Session["giohang"])
            {
                itemList.items.Add(new Item()
                {
                    name = item.Ten,
                    currency = "USD",
                    price = item.Gia.ToString(),
                    quantity = item.SoLuong.ToString(),
                    sku = "Size: " + item.Size
                });
                tongtien += (item.Gia * item.SoLuong);
                //subtotal += (item.Gia * item.SoLuong);
            }
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "25",
                subtotal = tongtien.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = (tongtien + double.Parse(details.shipping)).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypaloder}",
                invoice_number = paypaloder.ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
        //-----------Payment------------------------------
        public ActionResult PayMent()
        {
            if (Session["infocus"] != null)
            {
                Register form = Session["infocus"] as Register;
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                try
                {
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {  
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Shopping/PayMent?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                paypalRedirectUrl = lnk.href;
                            }
                        }
                        Session.Add(guid, createdPayment.id);
                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string); 
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return RedirectToAction("CheckoutFail");
                        }
                        //-----------------
                        Customer c = new Customer();
                        int i = 0;
                        foreach (var item in db.customer)
                        {
                            if (item.Email == form.Email)
                            {
                                i = 1;
                                break;
                            }
                        }
                        if (i != 1)
                        {
                            c.Name = form.Name;
                            c.Image = "anh-dai-dien-FB-200.jpg";
                            c.Email = form.Email;
                            c.Phone = form.Phone;
                            c.Address = form.Address;
                            c.Status = true;
                            db.customer.Add(c);
                            db.SaveChanges();
                        }
                        else
                        {
                            string Email = form.Email;
                            c = db.customer.Where(m => m.Email == Email).FirstOrDefault();
                            //if (c.Status == false)
                            //{
                            //    return View();
                            //}
                        }
                        Bill b = new Bill();
                        b.IDCus = c.IDCus;
                        b.DateCreated = DateTime.Now;
                        b.ShippingCost = 25;
                        b.BillTotal = 0;
                        b.PayMentStatus = 0;
                        b.OrderStatus = 1;
                        db.Bill.Add(b);
                        List<GioHang> lgh = (List<GioHang>)Session["giohang"];
                        double total = 0;
                        int quanitysold = 0;
                        Product product = new Product();
                        foreach (var item in lgh)
                        {
                            BillDetails bd = new BillDetails();
                            bd.IDBill = b.IDBill;
                            double tongtien = 0;
                            bd.IDProduct = item.ID;
                            bd.Quantity = item.SoLuong;
                            bd.UnitPrice = item.Gia;
                            bd.Size = item.Size;
                            tongtien += (item.Gia * item.SoLuong);
                            db.billDetails.Add(bd);
                            total += tongtien;
                            quanitysold += item.SoLuong;
                            product = db.products.Where(m => m.IDProduct == item.ID).FirstOrDefault();
                            product.QuantitySold += item.SoLuong;
                            product.QuantityRemaining -= item.SoLuong;
                            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            var size = db.size_Products.Where(m => m.IDPro == item.ID && m.size == item.Size).FirstOrDefault();
                            size.Quantity -= item.SoLuong;
                            db.Entry(size).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        total += 25;
                        b.BillTotal = total;
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        Session["madonhang"] = b.IDBill;
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Template/newoder.html"));
                        content = content.Replace("{{NameProduct}}", lgh[0].Ten);
                        content = content.Replace("{{Quantity}}", lgh[0].SoLuong.ToString());
                        content = content.Replace("{{Price}}", lgh[0].Gia.ToString());


                        content = content.Replace("{{HinhAnh}}", lgh[0].HinhAnh);
                        content = content.Replace("{{Name}}", c.Name);
                        content = content.Replace("{{Email}}", c.Email);
                        content = content.Replace("{{Phone}}", c.Phone);
                        content = content.Replace("{{Address}}", c.Address);
                        content = content.Replace("{{Total}}", total.ToString());

                        new MailHelper().SenMail(c.Email, "Đơn hàng mới", content);
                    }
                }
                catch (Exception ex)
                {
                    return View("FailureView");
                }
            }    
            return RedirectToAction("CheckoutSuccess");
        }
        public ActionResult CheckoutFail()
        {
            return View();
        }
        public ActionResult CheckoutSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TraCuuDonHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TraCuuDonHang(FormCollection form)
        {
            int MaDonHang = int.Parse(form["MaDonHang"]);
            string Email = form["Email"];
            List<Bill> lb = new List<Bill>();
            foreach(var item in db.Bill)
            {
                lb.Add(item);
            }
            foreach (var item in lb)
            {
                if (MaDonHang == item.IDBill)
                {
                    var cus = db.customer.Where(m => m.Email == Email || m.Phone == Email).FirstOrDefault();
                    if (cus != null)
                    {
                        Session["ThongTinKhachHang"] = cus;
                        Session["ThongTinDonHang"] = item;
                        Session["TrangThaiDonHang"] = item.OrderStatus;
                        Session["TrangThaiThanhToan"] = item.PayMentStatus;
                        return RedirectToAction("ThongTinDonHang");
                    }
                }
            }
            return View();
        }
        public ActionResult ThongTinDonHang()
        {
            return View();
        }
        public PartialViewResult ThongTinKhachHang()
        {
            return PartialView("ThongTinKhachHang");
        }
        public PartialViewResult ChiTietDonHang()
        {
            List<GioHang> lbd = new List<GioHang>();
            List<BillDetails> listbd = new List<BillDetails>();
            foreach (var item in db.billDetails)
            {
                listbd.Add(item);
            }
            foreach(var item in listbd)
            {
                if (item.IDBill == (Session["ThongTinDonHang"] as Bill).IDBill)
                {
                    var pro = db.products.Where(m => m.IDProduct == item.IDProduct).FirstOrDefault();
                    GioHang g = new GioHang();
                    g.Ten = pro.NameProduct;
                    g.Gia = item.UnitPrice;
                    g.SoLuong = item.Quantity;
                    g.HinhAnh = pro.Image1;
                    g.Size = item.Size;
                    lbd.Add(g);
                }
            }
            Session["ChiTietDonHang"] = lbd;
            return PartialView("ChiTietDonHang");
        }
    }
}