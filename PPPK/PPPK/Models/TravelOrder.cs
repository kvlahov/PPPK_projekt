using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    [Table("TravelOrder")]
    public class TravelOrder
    {
        [Key]
        public int IDTravelOrder { get; set; }
        public int DriverID { get; set; }

        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; set; }
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }
        public int TravelOrderTypeID { get; set; }

        [ForeignKey("TravelOrderTypeID")]
        public virtual TravelOrderType TravelOrderType { get; set; }
        public int? CityStartId { get; set; }

        [ForeignKey("CityStartId")]
        public virtual City CityStart { get; set; }
        public int? CityEndId { get; set; }

        [ForeignKey("CityEndId")]
        public virtual City CityEnd { get; set; }
        public int ExpectedNumberOfDays { get; set; }

        [StringLength(100)]
        public string ReasonForTravel { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalCost { get; set; }
        public DateTime? DocumentDate { get; set; }
        public virtual ICollection<RouteInfo> RouteInfos { get; set; }
    }
}