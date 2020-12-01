using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int IDCategory { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<Product> products { get; set; }
    }
}