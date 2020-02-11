using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class TravelOrder
    {
        [Key]
        public long IDTravelOrder { get; set; }
        public long DriverID { get; set; }
        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; set; }
        public long VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }
        public long TravelOrderTypeID { get; set; }
        [ForeignKey("TravelOrderTypeID")]
        public virtual TravelOrderType TravelOrderType { get; set; }
        public long CityStartId { get; set; }
        [ForeignKey("CityStartId")]
        public virtual City CityStart { get; set; }
        public long CityEndId { get; set; }
        [ForeignKey("CityEndId")]
        public virtual City CityEnd { get; set; }
        public int ExpectedNumberOfDays { get; set; }
        public string ReasonForTravel { get; set; }
        public double TotalCost { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}