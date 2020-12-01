using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using WebBanQuanAo.DAL;
using System.IO;
using WebBanQuanAo.Class;
using PagedList;

namespace WebBanQuanAo.Controllers
{
    public class ProductsController : Controller
    {
        WebBanHangContext db = new WebBanHangContext();
        //Admin-------------------------------------------------------
        public ActionResult Index(int? size, int? page, string strSearch)
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
            var ListProduct = (from l in db.products select l).OrderBy(x => x.IDProduct);
            if(!string.IsNullOrEmpty(strSearch))
                ListProduct = (from l in db.products where l.NameProduct.Contains(strSearch) || l.Description.Contains(strSearch) select l).OrderBy(x => x.IDProduct);
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.strSearch = strSearch;
            return View(ListProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<_Category> listcate = new List<_Category>();
            foreach (var item in db.categories.ToList())
            {
                _Category c = new _Category();
                c.IDCategory = item.IDCategory;
                c.Name = item.Name;
                listcate.Add(c);
            }
            ViewBag.IDCategory = new SelectList(listcate, "IDCategory", "Name", null);
            List<ThuongHieu> listth = new List<ThuongHieu>();
            foreach (var item in db.thuongHieus.ToList())
            {
                ThuongHieu th = new ThuongHieu();
                th.ID = item.ID;
                th.Name = item.Name;
                listth.Add(th);
            }
            ViewBag.IDThuongHieu = new SelectList(listth, "ID", "Name", null);
            List<ChatLieu> listcl = new List<ChatLieu>();
            foreach (var item in db.chatLieus.ToList())
            {
                ChatLieu cl = new ChatLieu();
                cl.ID = item.ID;
                cl.Ten = item.Ten;
                listcl.Add(cl);
            }
            ViewBag.IDChatLieu = new SelectList(listcl, "ID", "Ten", null);
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product pro, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (Image1 != null)
                {
                    var filename1 = Path.GetFileName(Image1.FileName);
                    pro.Image1 = filename1;
                    string path1 = Path.Combine(Server.MapPath("~/Content/Images/"), filename1);
                    Image1.SaveAs(path1);
                }
                if (Image2 != null)
                {
                    var filename2 = Path.GetFileName(Image2.FileName);
                    pro.Image2 = filename2;
                    string path2 = Path.Combine(Server.MapPath("~/Content/Images/"), filename2);
                    Image2.SaveAs(path2);
                }
                if (Image3 != null)
                {
                    var filename3 = Path.GetFileName(Image3.FileName);
                    pro.Image3 = filename3;
                    string path3 = Path.Combine(Server.MapPath("~/Content/Images/"), filename3);
                    Image3.SaveAs(path3);
                }
                if (Image4 != null)
                {
                    var filename4 = Path.GetFileName(Image4.FileName);
                    pro.Image4 = filename4;
                    string path4 = Path.Combine(Server.MapPath("~/Content/Images/"), filename4);
                    Image4.SaveAs(path4);
                }
                if (Image5 != null)
                {
                    var filename5 = Path.GetFileName(Image5.FileName);
                    pro.Image5 = filename5;
                    string path5 = Path.Combine(Server.MapPath("~/Content/Images/"), filename5);
                    Image5.SaveAs(path5);
                }
                pro.QuantitySold = 0;
                db.products.Add(pro);
                int quantity = 0;
                for(int i = 1; i<= 10;i++)
                {
                    if (form["size" + i] != null && form["SL" + i] != null)
                    {
                        Size_Product sp = new Size_Product();
                        sp.IDPro = pro.IDProduct;
                        sp.size = form["size" + i];
                        sp.Quantity = int.Parse(form["SL" + i]);
                        quantity += sp.Quantity;
                        db.size_Products.Add(sp);
                    }
                }
                pro.QuantityRemaining = quantity;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<_Category> listcate = new List<_Category>();
            foreach (var item in db.categories.ToList())
            {
                _Category c = new _Category();
                c.IDCategory = item.IDCategory;
                c.Name = item.Name;
                listcate.Add(c);
            }
            ViewBag.IDCategory = new SelectList(listcate, "IDCategory", "Name", null);
            List<ThuongHieu> listth = new List<ThuongHieu>();
            foreach (var item in db.thuongHieus.ToList())
            {
                ThuongHieu th = new ThuongHieu();
                th.ID = item.ID;
                th.Name = item.Name;
                listth.Add(th);
            }
            ViewBag.IDThuongHieu = new SelectList(listth, "ID", "Name", null);
            List<ChatLieu> listcl = new List<ChatLieu>();
            foreach (var item in db.chatLieus.ToList())
            {
                ChatLieu cl = new ChatLieu();
                cl.ID = item.ID;
                cl.Ten = item.Ten;
                listcl.Add(cl);
            }
            ViewBag.IDChatLieu = new SelectList(listcl, "ID", "Ten", null);
            return View();
        }
        public ActionResult DeleteProduct(Product pro, int id)
        {
            pro = db.products.Where(m => m.IDProduct == id).FirstOrDefault();
            foreach(var item in db.size_Products)
            {
                if (item.IDPro == pro.IDProduct)
                    db.size_Products.Remove(item);
            }
            db.products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var sp1 = db.products.Where(m => m.IDProduct == id).FirstOrDefault();
            Session["sp1"] = sp1;
            List<_Category> listcate = new List<_Category>();
            foreach (var item in db.categories.ToList())
            {
                _Category c = new _Category();
                c.IDCategory = item.IDCategory;
                c.Name = item.Name;
                listcate.Add(c);
            }
            ViewBag.IDCategory = new SelectList(listcate, "IDCategory", "Name", null);
            List<ThuongHieu> listth = new List<ThuongHieu>();
            foreach (var item in db.thuongHieus.ToList())
            {
                ThuongHieu th = new ThuongHieu();
                th.ID = item.ID;
                th.Name = item.Name;
                listth.Add(th);
            }
            ViewBag.IDThuongHieu = new SelectList(listth, "ID", "Name", null);
            List<ChatLieu> listcl = new List<ChatLieu>();
            foreach (var item in db.chatLieus.ToList())
            {
                ChatLieu cl = new ChatLieu();
                cl.ID = item.ID;
                cl.Ten = item.Ten;
                listcl.Add(cl);
            }
            ViewBag.IDChatLieu = new SelectList(listcl, "ID", "Ten", null);
            return View(db.products.Where(m => m.IDProduct == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditProduct(Product pro, int id, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Product sp1 = Session["sp1"] as Product;
                if (Image1 != null)
                {
                    var filename1 = Path.GetFileName(Image1.FileName);
                    pro.Image1 = filename1;
                    string path1 = Path.Combine(Server.MapPath("~/Content/Images/"), filename1);
                    Image1.SaveAs(path1);
                }
                else
                {
                    pro.Image1 = sp1.Image1;
                }
                    
                if (Image2 != null)
                {
                    var filename2 = Path.GetFileName(Image2.FileName);
                    pro.Image2 = filename2;
                    string path2 = Path.Combine(Server.MapPath("~/Content/Images/"), filename2);
                    Image2.SaveAs(path2);
                }
                else
                {
                    pro.Image2 = sp1.Image2;
                }
                if (Image3 != null)
                {
                    var filename3 = Path.GetFileName(Image3.FileName);
                    pro.Image3 = filename3;
                    string path3 = Path.Combine(Server.MapPath("~/Content/Images/"), filename3);
                    Image3.SaveAs(path3);
                }
                else
                {
                    pro.Image3 = sp1.Image3;
                }
                if (Image4 != null)
                {
                    var filename4 = Path.GetFileName(Image4.FileName);
                    pro.Image4 = filename4;
                    string path4 = Path.Combine(Server.MapPath("~/Content/Images/"), filename4);
                    Image4.SaveAs(path4);
                }
                else
                {
                    pro.Image4 = sp1.Image4;
                }
                if (Image5 != null)
                {
                    var filename5 = Path.GetFileName(Image5.FileName);
                    pro.Image5 = filename5;
                    string path5 = Path.Combine(Server.MapPath("~/Content/Images/"), filename5);
                    Image5.SaveAs(path5);
                }
                else
                {
                    pro.Image5 = sp1.Image5;
                }

                foreach (var item in db.size_Products)
                {
                    if (item.IDPro == pro.IDProduct)
                        db.size_Products.Remove(item);
                }
                int quantity = 0;
                for (int i = 1; i <= 10; i++)
                {
                    if (form["size" + i] != null && form["SL" + i] != null && form["SL"+i] != "")
                    {
                        Size_Product sp = new Size_Product();
                        sp.IDPro = pro.IDProduct;
                        sp.size = form["size" + i];
                        sp.Quantity = int.Parse(form["SL" + i]);
                        quantity += sp.Quantity;
                        db.size_Products.Add(sp);
                    }
                }
                pro.QuantityRemaining = quantity;
                db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            List<_Category> listcate = new List<_Category>();
            foreach (var item in db.categories.ToList())
            {
                _Category c = new _Category();
                c.IDCategory = item.IDCategory;
                c.Name = item.Name;
                listcate.Add(c);
            }
            ViewBag.IDCategory = new SelectList(listcate, "IDCategory", "Name", null);
            List<ThuongHieu> listth = new List<ThuongHieu>();
            foreach (var item in db.thuongHieus.ToList())
            {
                ThuongHieu th = new ThuongHieu();
                th.ID = item.ID;
                th.Name = item.Name;
                listth.Add(th);
            }
            ViewBag.IDThuongHieu = new SelectList(listth, "ID", "Name", null);
            List<ChatLieu> listcl = new List<ChatLieu>();
            foreach (var item in db.chatLieus.ToList())
            {
                ChatLieu cl = new ChatLieu();
                cl.ID = item.ID;
                cl.Ten = item.Ten;
                listcl.Add(cl);
            }
            ViewBag.IDChatLieu = new SelectList(listcl, "ID", "Ten", null);
            return RedirectToAction("Index");
        }
        public PartialViewResult Size()
        {
            return PartialView("Size");
        }
        //Client-------------------------------------------------------
        [HttpGet]
        public ActionResult ChiTietSanPam(int id)
        {
            Session["IDPro"] = id;
            List<Size_Product> listsize = new List<Size_Product>();
            foreach (var item in db.size_Products)
            {
                if (item.IDPro == id)
                    listsize.Add(item);
            }
            ViewBag.listsize = listsize;
            return View(db.products.Where(m => m.IDProduct == id).FirstOrDefault());
        }
        public ActionResult ListProduct(int id)
        {
            List<Product> listpro = new List<Product>();
            var cate = db.categories.Where(m => m.IDCategory == id).FirstOrDefault();
            if (cate != null)
            {
                foreach (var sp in db.products.ToList())
                {
                    if (sp.IDCategory == cate.IDCategory)
                        listpro.Add(sp);
                }
                return View(listpro);
            }
            return View();
        }
    }
}