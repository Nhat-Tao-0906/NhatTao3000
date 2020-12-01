using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class ThuongHieu
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Product> products { get; set; }
    }
}