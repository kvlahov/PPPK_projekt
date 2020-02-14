using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    [Table("RouteInfo")]
    public class RouteInfo
    {
        [Key]
        public int IDRouteInfo { get; set; }
        [Required]
        public DateTime? DateTimeStart { get; set; }

        [Required]
        public DateTime? DateTimeEnd { get; set; }
        public double? LatitudeStart { get; set; }
        public double? LongitudeStart { get; set; }
        public double? LatitudeEnd { get; set; }
        public double? LongitudeEnd { get; set; }
        public double? DistanceInKm { get; set; }
        public double? AverageSpeed { get; set; }
        public double? FuelExpense { get; set; }
        public int? TravelOrderID { get; set; }
        [ForeignKey("TravelOrderID")]
        public virtual TravelOrder TravelOrder { get; set; }
    }
}