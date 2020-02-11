using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class Vehicle
    {
        [Key]
        public long IDVehicle { get; set; }
        public string Registration { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int YearManufactured { get; set; }
        public double InitialKilometres { get; set; }
        public bool IsAvailable { get; set; } = true;
        public virtual ICollection<ServiceInfo> ServiceInfos { get; set; }
    }
}