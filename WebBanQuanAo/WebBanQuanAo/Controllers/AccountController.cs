using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;
using WebBanQuanAo.Class;

namespace WebBanQuanAo.Controllers
{
    public class AccountController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: Account
        public ActionResult Account()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Login()
        {
            return PartialView("Login");
        }
        [HttpPost]
        public ActionResult Login(Account a)
        {
            foreach(var item in db.account.ToList())
            {
                if (item.UserName == a.UserName && item.PassWord == a.PassWord)
                {
                    return RedirectToAction("Home", "Home");
                }
                
            }
            ViewBag.errorlogin = "Username and password is not correct";
            return RedirectToAction("Account");
        }
        public PartialViewResult Input_Customer(Customer c)
        {
            return PartialView("Input_Customer");
        }
        public PartialViewResult Input_Account()
        {
            return PartialView("Input_Account");
        }
        [HttpGet]
        public PartialViewResult Register()
        {
            return PartialView("Register");
        }
        [HttpPost]
        public ActionResult Register(Register r)
        {
            var cus = db.customer.Where(m => m.Email == r.Email).FirstOrDefault();
            if (cus != null)
            {
                Account a = new Account();
                a.IDCus = cus.IDCus;
                a.UserName = r.UserName;
                a.PassWord = r.PassWord;
                a.ConfirmPassWord = r.ConfirmPassWord;
                db.account.Add(a);
                db.SaveChanges();
            }
            else
            {
                Customer c = new Customer();
                c.Name = r.Name;
                c.Email = r.Email;
                c.Phone = r.Phone;
                c.Address = r.Address;
                db.customer.Add(c);
                db.SaveChanges();
                Account a = new Account();
                a.IDCus = c.IDCus;
                a.UserName = r.UserName;
                a.PassWord = r.PassWord;
                a.ConfirmPassWord = r.ConfirmPassWord;
                db.account.Add(a);
                db.SaveChanges();
            }
            return RedirectToAction("Account", "Account");
        }
    }
}