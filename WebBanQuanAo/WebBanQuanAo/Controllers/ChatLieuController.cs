using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;

namespace WebBanQuanAo.Controllers
{
    public class ChatLieuController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: ChatLieu
        public ActionResult Index()
        {
            return View(db.chatLieus.ToList());
        }

        public ActionResult ThemChatLieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemChatLieu(ChatLieu cl)
        {
            if (ModelState.IsValid)
            {
                db.chatLieus.Add(cl);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteChatLieu(ChatLieu cl, int id)
        {
            cl = db.chatLieus.Where(m => m.ID == id).FirstOrDefault();
            db.chatLieus.Remove(cl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditChatLieu(int id)
        {
            return View(db.chatLieus.Where(m => m.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditChatLieu(ChatLieu cl)
        {
            db.Entry(cl).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult AddCategory_Modal()
        //{
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult AddCategory_Modal(Category cate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.categories.Add(cate);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}