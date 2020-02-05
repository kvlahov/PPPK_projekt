using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class TravelOrder
    {
        public long IDTravelOrder { get; set; }
        public long DriverID { get; set; }
        public long VehicleID { get; set; }
        public long TravelOrderTypeID { get; set; }
        public long RouteInfoID { get; set; }
        public long FuelInfoID { get; set; }
        public int ExpectedNumberOfDays { get; set; }
        public string ReasonForTravel { get; set; }
        public double TotalCost { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}