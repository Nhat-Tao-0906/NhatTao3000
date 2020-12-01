using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.DAL
{
    public class WebBanHangContext:DbContext
    {
        public WebBanHangContext():base("name = DbQuanLyBanHang") { }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> billDetails { get; set; }
        //public DbSet<Bill_Cus> bill_Cus { get; set; }
        public DbSet<ChatLieu> chatLieus { get; set; }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<Size_Product> size_Products { get; set; }
    }
}