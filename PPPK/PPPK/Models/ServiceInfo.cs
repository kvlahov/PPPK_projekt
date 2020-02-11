using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class ServiceInfo
    {
        [Key]
        public long IDService { get; set; }
        public long VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }
        public DateTime DatetimeService { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
    }
}