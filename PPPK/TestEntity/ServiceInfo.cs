namespace TestEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceInfo")]
    public partial class ServiceInfo
    {
        [Key]
        public int IDService { get; set; }

        public int VehicleID { get; set; }

        public DateTime? DatetimeService { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
