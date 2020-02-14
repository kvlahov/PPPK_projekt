using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PPPK.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public int IDVehicle { get; set; }

        [Required]
        [StringLength(30)]
        public string Registration { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Display(Name = "Year Manufactured")]
        public short YearManufactured { get; set; }

        [Display(Name = "Initial Kilometres")]
        public double InitialKilometres { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<ServiceInfo> ServiceInfos { get; set; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<TravelOrder> TravelOrders { get; set; }
    }
}