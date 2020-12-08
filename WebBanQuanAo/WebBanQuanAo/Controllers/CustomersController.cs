using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
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
            var cus = db.customer.Where(m => m.IDCus == id).FirstOrDefault();
            if (cus.Status == false)
                cus.Status = true;
            else
                cus.Status = false;
            db.Entry(cus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang");
        }
        public ActionResult ThongTinKhachHang()
        {
            if (Session["idcusReview"] != null)
            {
                int id = int.Parse(Session["idcusReview"].ToString());
                var cus = db.customer.Where(m => m.IDCus == id).FirstOrDefault();
                return View(cus);
            }
            else
                return RedirectToAction("Account", "Account");
        }
        public ActionResult EditCustomer(Customer cus, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(Session["idcusReview"].ToString());
                cus.IDCus = id;
                if (Image != null)
                {
                    var filename = Path.GetFileName(Image.FileName);
                    cus.Image = filename;
                    string path1 = Path.Combine(Server.MapPath("~/Content/Images/"), filename);
                    Image.SaveAs(path1);
                }
                else
                {
                    cus.Image = cus.Image;
                }
                db.Entry(cus).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }    
            return RedirectToAction("ThongTinKhachHang");
        }
        [HttpPost]
        public ActionResult ThayDoiMatKhau(FormCollection form)
        {
            int id = int.Parse(Session["idcusReview"].ToString());
            var account = db.account.Where(m => m.IDCus == id).FirstOrDefault();
            if (form["ConfirmPassWord"] != null && form["PassWord"] != null)
            {
                if (form["ConfirmPassWord"] == form["PassWord"])
                {
                    account.PassWord = form["PassWord"].ToString();
                    account.ConfirmPassWord = form["ConfirmPassWord"].ToString();
                    db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.errorconfirm = "Mật khẩu nhập lại không đúng";
                }
            }
            else
            {
                ViewBag.errorpassword = "Mật khẩu không được bỏ trống";
                ViewBag.errorconfirmpassword = "Mật khẩu nhập lại không được bỏ trống";
            }    
            return RedirectToAction("ThongTinKhachHang");
        }
        //public PartialViewResult LichSuMuaHang()
        //{
        //    return PartialView("LichSuMuaHang");
        //}
    }
}