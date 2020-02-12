using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    [Table("FuelInfo")]
    public class FuelInfo
    {
        [Key]
        public int IDFuelInfo { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public double? Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatePurchased { get; set; }

        public int? TravelOrderID { get; set; }

        [ForeignKey("TravelOrderID")]
        public virtual TravelOrder TravelOrder { get; set; }
    }
}