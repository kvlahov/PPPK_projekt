using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    [Table("ServiceInfo")]
    public class ServiceInfo
    {
        [Key]
        public int IDService { get; set; }

        public int VehicleID { get; set; }

        [Display(Name ="Date of service")]
        [Required]
        public DateTime? DatetimeService { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }
    }
}