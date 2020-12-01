using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("customer")]
        public int IDCus { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Confirm password does not match")]
        [Required]
        public string ConfirmPassWord { get; set; }
        public bool Status { get; set; }
        public Customer customer { get; set; }
    }
}