using PPPK.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPPK.ViewModels
{
    public class VehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        [Display(Name ="Total kilometers")]
        public double TotalKm { get; set; }
        [Display(Name = "Average Speed")]
        public double AverageSpeed { get; set; }
        public IList<ServiceInfo> Services { get; set; }
    }
}