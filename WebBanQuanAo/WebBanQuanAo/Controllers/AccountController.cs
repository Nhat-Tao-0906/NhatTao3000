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
            if(Session["idcusReview"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ThongTinKhachHang","Customers");
            }
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
                if (item.UserName == a.UserName && item.PassWord == a.PassWord && item.Status == true)
                {
                    Session["idcusReview"] = item.IDCus;
                    return RedirectToAction("Home", "Home");
                }
                
            }
            ViewBag.errorlogin = "Username and password is not correct";
            return RedirectToAction("Account");
        }

        public ActionResult SignOut()
        {
            Session["idcusReview"] = null;
            return RedirectToAction("Home", "Home");
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
            var user = db.account.Where(m => m.UserName == r.UserName).FirstOrDefault();
            if (user == null)
            {
                var cus = db.customer.Where(m => m.Email == r.Email).FirstOrDefault();
                if (cus != null)
                {

                    var account = db.account.Where(m => m.IDCus == cus.IDCus).FirstOrDefault();
                    if (account == null)
                    {
                        Account a = new Account();
                        a.IDCus = cus.IDCus;
                        a.UserName = r.UserName;
                        a.PassWord = r.PassWord;
                        a.ConfirmPassWord = r.ConfirmPassWord;
                        a.Status = true;
                        db.account.Add(a);
                        db.SaveChanges();
                    }
                    else
                        return RedirectToAction("Account", "Account");
                }
                else
                {
                    Customer c = new Customer();
                    c.Name = r.Name;
                    c.Email = r.Email;
                    c.Phone = r.Phone;
                    c.Address = r.Address;
                    c.Status = true;
                    c.Image = "anh-dai-dien-FB-200.jpg";
                    db.customer.Add(c);
                    db.SaveChanges();
                    Account a = new Account();
                    a.IDCus = c.IDCus;
                    a.UserName = r.UserName;
                    a.PassWord = r.PassWord;
                    a.ConfirmPassWord = r.ConfirmPassWord;
                    a.Status = true;
                    db.account.Add(a);
                    db.SaveChanges();
                }
                return RedirectToAction("Account", "Account");
            }
            else
            {
                ViewBag.errorusername = "";
                return RedirectToAction("Account", "Account");
            }
        }
    }
}