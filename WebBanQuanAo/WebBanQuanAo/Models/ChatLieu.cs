using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    public class ChatLieu
    {
        [Key]
        public int ID { get; set; }
        public string Ten { get; set; }
        public ICollection<Product> products { get; set; }
    }
}