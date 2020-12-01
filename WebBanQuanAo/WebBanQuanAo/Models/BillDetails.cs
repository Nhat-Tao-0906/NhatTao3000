using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class BillDetails
    {
        [Key]
        public int IDBillDetails { get; set; }
        [Required]
        [ForeignKey("product")]
        public int IDProduct { get; set; }
        [Required]
        [ForeignKey("bill")]
        public int IDBill { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public string Size { get; set; }
        //[Required]
        //public double Total { get; set; }
        public Product product { get; set; }
        public Bill bill { get; set; }

    }
}