using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int IDProduct { get; set; }

        [ForeignKey("category")]
        public int IDCategory { get; set; }
        [Required]

        [ForeignKey("chatlieu")]
        public int IDChatLieu { get; set; }
        [Required]

        [ForeignKey("thuonghieu")]
        public int IDThuongHieu { get; set; }
        [Required]
        public string NameProduct { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        [Required]
        public double Price { get; set; }
        //[Required]
        //public string Size { get; set; }
        //[Required]
        //public int Quantity { get; set; }
        public int? QuantitySold { get; set; }
        public int? QuantityRemaining { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ImportDate { get; set; }
        public Category category { get; set; }
        public ChatLieu chatlieu { get; set; }
        public ThuongHieu thuonghieu { get; set; }
        //public Size_Product size_Product { get; set; }
        public ICollection<BillDetails> billDetails { get; set; }
        public ICollection<Size_Product> size_Products { get; set; }
        public ICollection<Reviews> reviews { get; set; }
    }
}