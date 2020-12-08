using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Customer
    {
        [Key]
        public int IDCus { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Image { get; set; }
        [Required]
        public string Address { get; set; }
        public bool Status { get; set; }
        public ICollection<Bill> bill { get; set; }
        public ICollection<Reviews> reviews { get; set; }
    }
}