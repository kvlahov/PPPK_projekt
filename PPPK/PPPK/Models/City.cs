using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class City
    {
        [Key]
        public long IDCity { get; set; }
        public string Name { get; set; }
    }
}