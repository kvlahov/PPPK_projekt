using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class ServiceInfo
    {
        public long IDService { get; set; }
        public long VehicleID { get; set; }
        public DateTime DatetimeService { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
    }
}