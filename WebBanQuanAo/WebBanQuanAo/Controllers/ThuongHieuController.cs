using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;

namespace WebBanQuanAo.Controllers
{
    public class ThuongHieuController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: ThuongHieu
        public ActionResult Index()
        {
            return View(db.thuongHieus.ToList());
        }
        public ActionResult ThemThuongHieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemThuongHieu(ThuongHieu th)
        {
            if (ModelState.IsValid)
            {
                db.thuongHieus.Add(th);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteThuongHieu(ThuongHieu th, int id)
        {
            th = db.thuongHieus.Where(m => m.ID == id).FirstOrDefault();
            db.thuongHieus.Remove(th);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditThuongHieu(int id)
        {
            return View(db.thuongHieus.Where(m => m.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditThuongHieu(ThuongHieu th)
        {
            db.Entry(th).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}