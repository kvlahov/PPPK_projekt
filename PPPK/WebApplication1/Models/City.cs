using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        public int IDCity { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}