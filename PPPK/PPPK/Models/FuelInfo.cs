using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class FuelInfo
    {
        [Key]
        public long IDFuelInfo { get; set; }
        public string Location { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public DateTime DatePurchased { get; set; }
        public long TravelOrderID { get; set; }
        [ForeignKey("TravelOrderID")]
        public virtual TravelOrder TravelOrder { get; set; }
    }
}