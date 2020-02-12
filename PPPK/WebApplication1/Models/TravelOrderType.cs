using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("TravelOrderType")]
    public class TravelOrderType
    {
        [Key]
        public int IDTravelOrderType { get; set; }

        [Required]
        [StringLength(30)]
        public string Type { get; set; }
    }
}