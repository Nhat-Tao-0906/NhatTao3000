using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;
using PagedList;
using WebBanQuanAo.Class;
using System.Web.Routing;

namespace WebBanQuanAo.Controllers
{
    public class BillController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: Bill
        public ActionResult Bill(int? size, int? page, string strSearch)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });
            foreach(var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            var ListBill = (from l in db.Bill select l).OrderBy(x => x.IDBill);
            if (!string.IsNullOrEmpty(strSearch))
                ListBill = (from l in db.Bill select l).OrderBy(x => x.IDBill);
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.strSearch = strSearch;
            return View(ListBill.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChiTietDonHang(int id)
        {
            var bill = db.Bill.Where(m => m.IDBill == id).FirstOrDefault();
            var cus = db.customer.Where(m => m.IDCus == bill.IDCus).FirstOrDefault();
            Session["KH"] = cus;
            Session["ThanhToan"] = bill.PayMentStatus;
            Session["TrangThai"] = bill.OrderStatus;
            List<GioHang> listdonhang = new List<GioHang>();
            List<BillDetails> lbd = new List<BillDetails>();
            foreach(var item in db.billDetails)
            {
                lbd.Add(item);
            }
            foreach(var item in lbd)
            {
                if (item.IDBill == id)
                {
                    var pro = db.products.Where(m => m.IDProduct == item.IDProduct).FirstOrDefault();
                    GioHang g = new GioHang();
                    g.Ten = pro.NameProduct;
                    g.Gia = item.UnitPrice;
                    g.SoLuong = item.Quantity;
                    g.Size = item.Size;
                    g.HinhAnh = pro.Image1;
                    listdonhang.Add(g);
                }
            }
            Session["Dh_Gh"] = listdonhang;
            Session["DH"] = bill;
            return View();
        }
        public PartialViewResult ThongTinDonHang()
        {
            return PartialView("ThongTinDonHang");
        }
        public PartialViewResult ThongTinKhachHang()
        {
            return PartialView("ThongTinKhachHang");
        }
        public ActionResult CapNhatDonHang(FormCollection form)
        {
            int id = (Session["DH"] as Bill).IDBill;
            var bill = db.Bill.Where(m => m.IDBill == id).FirstOrDefault();
            bill.OrderStatus = form["name"];
            db.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("ChiTietDonHang", "Bill", id);
            return RedirectToAction("ChiTietDonHang", new RouteValueDictionary(new { controller = "Bill", action = "ChiTietDonHang", Id = id }));
        }
        public ActionResult HuyDon()
        {
            int id = (Session["DH"] as Bill).IDBill;
            var bill = db.Bill.Where(m => m.IDBill == id).FirstOrDefault();
            bill.OrderStatus = "0";
            db.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Bill");
        }
    }
}