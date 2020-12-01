using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.DAL;

namespace WebBanQuanAo.Controllers
{
    public class DashboardController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Getdata()
        {
            DoanhThuTheoThang dt = new DoanhThuTheoThang();
            foreach(var item in db.Bill)
            {
                if (item.DateCreated.ToString().Split('/')[0] == "1")
                    dt.Thang1 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "2")
                    dt.Thang2 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "3")
                    dt.Thang3 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "4")
                    dt.Thang4 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "5")
                    dt.Thang5 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "6")
                    dt.Thang6 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "7")
                    dt.Thang7 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "8")
                    dt.Thang8 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "9")
                    dt.Thang9 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "10")
                    dt.Thang10 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "11")
                    dt.Thang11 += item.BillTotal;
                else if (item.DateCreated.ToString().Split('/')[0] == "12")
                    dt.Thang12 += item.BillTotal;
            }
            return Json(dt,JsonRequestBehavior.AllowGet);
        }
        public class DoanhThuTheoThang
        {
            public double Thang1;
            public double Thang2;
            public double Thang3;
            public double Thang4;
            public double Thang5;
            public double Thang6;
            public double Thang7;
            public double Thang8;
            public double Thang9;
            public double Thang10;
            public double Thang11;
            public double Thang12;
            public DoanhThuTheoThang()
            {
                this.Thang1 = 0;
                this.Thang2 = 0;
                this.Thang3 = 0;
                this.Thang4 = 0;
                this.Thang5 = 0;
                this.Thang6 = 0;
                this.Thang7 = 0;
                this.Thang8 = 0;
                this.Thang9 = 0;
                this.Thang10 = 0;
                this.Thang11 = 0;
                this.Thang12 = 0;
            }
        }
    }
}