using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;


namespace WebBanQuanAo.Controllers
{
    public class CategorysController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();

        //Admin-------------------------------------------------------------------
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category cate)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(cate);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(Category cate, int id)
        {
            cate = db.categories.Where(m => m.IDCategory == id).FirstOrDefault();
            db.categories.Remove(cate);
            db.SaveChanges();
            ViewBag.a = "a";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View(db.categories.Where(m => m.IDCategory == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditCategory(Category cate)
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddCategory_Modal()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddCategory_Modal(Category cate)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(cate);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //Client-------------------------------------------------------------------------------
    }
}