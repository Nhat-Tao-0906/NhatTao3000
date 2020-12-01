using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Bill_Cus
    {
        [Key]
        public int IDBill { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        //[Required]
        //public double ShippingCost { get; set; }
        [Required]
        public DateTime? PayDay { get; set; }
        public string HinhThucThanhToan { get; set; }
        public ICollection<BillDetails> billDetails { get; set; }
    }
}