using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Models
{
    public class Size_Product
    {
        [Key]
        public int IDSize_Product { get; set; }
        [Required]
        [ForeignKey("product")]
        public int IDPro { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string size { get; set; }
        public Product product { get; set; }
    }
}