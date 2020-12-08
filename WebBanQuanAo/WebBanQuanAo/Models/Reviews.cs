using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Models
{
    public class Reviews
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Cus")]
        public int IDCus { get; set; }
        [ForeignKey("product")]
        public int IDProduct { get; set; }
        [Required]
        public DateTime DateReview { get; set; }
        [Required]
        public int Ratio { get; set; }
        [Required]
        public string Content { get; set; }
        public Customer Cus { get; set; }
        public Product product { get; set; }
    }
}