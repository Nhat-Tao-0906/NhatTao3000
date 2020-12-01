using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;


namespace WebBanQuanAo.Controllers
{
    public class HomeController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        //Admin------------------------------------------------------------------

        //Client-----------------------------------------------------------------
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult FeaturedProducts()
        {
            List<Product> listpro = new List<Product>();
            var date = DateTime.Now;
            foreach (var item in db.products.ToList())
            {
                string ngay = date.ToString("dd/MM/yyyy").Split('/')[0];
                string ngaynhap = item.ImportDate.ToString("dd/MM/yyyy").Split('/')[0];
                if (int.Parse(ngay) - int.Parse(ngaynhap) < 10)
                {
                    listpro.Add(item);
                }
            }
            return PartialView(listpro);
        }
        public ActionResult trendProducts()
        {
            return PartialView("trendProducts.cshtml");
        }
        public ActionResult bestSellers()
        {
            return PartialView("bestSellers.cshtml");
        }
    }
}