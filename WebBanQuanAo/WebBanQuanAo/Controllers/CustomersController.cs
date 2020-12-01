using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.DAL;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class CustomersController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: Customers
        public ActionResult DanhSachKhachHang(int? size, int? page, string strSearch)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            var ListCus = (from l in db.customer select l).OrderBy(x => x.IDCus);
            if (!string.IsNullOrEmpty(strSearch))
                ListCus = (from l in db.customer where l.Name.Contains(strSearch) || l.Email.Contains(strSearch) || l.Phone.Contains(strSearch) || l.Address.Contains(strSearch) select l).OrderBy(x => x.IDCus);
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.strSearch = strSearch;
            return View(ListCus.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CapNhatTrangThai(int id)
        {
            //int id = int.Parse(ViewBag.id);
            var cus = db.customer.Where(m => m.IDCus == id).FirstOrDefault();
            if (cus.Status == false)
                cus.Status = true;
            else
                cus.Status = false;
            db.Entry(cus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang");
        }
    }
}