using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
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

        public short YearManufactured { get; set; }

        public double InitialKilometres { get; set; }

        public bool IsAvailable { get; set; }
        public virtual ICollection<ServiceInfo> ServiceInfos { get; set; }
    }
}