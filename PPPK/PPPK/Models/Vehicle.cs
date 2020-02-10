using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class Vehicle
    {
        public long IDVehicle { get; set; }
        public string Registration { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int YearManufactured { get; set; }
        public double InitialKilometres { get; set; }
        public bool IsAvailable { get; set; } = true;

    }
}