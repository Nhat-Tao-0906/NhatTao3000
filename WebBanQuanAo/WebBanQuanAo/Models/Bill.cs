using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Bill
    {
        [Key]
        public int IDBill { get; set; }
        [Required]
        [ForeignKey("customer")]
        public int IDCus { get; set; }
        [Required]
        public double ShippingCost { get; set; }
        [Required]
        public double BillTotal { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public int PayMentStatus { get; set; }
        [Required]
        public DateTime? DateCreated { get; set; }
        public Customer customer { get; set; }
        public ICollection<BillDetails> billDetails { get; set; }
    }
}