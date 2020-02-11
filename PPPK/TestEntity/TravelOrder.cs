namespace TestEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TravelOrder")]
    public partial class TravelOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelOrder()
        {
            FuelInfo = new HashSet<FuelInfo>();
            RouteInfo = new HashSet<RouteInfo>();
        }

        [Key]
        public int IDTravelOrder { get; set; }

        public int DriverID { get; set; }

        public int VehicleID { get; set; }

        public int TravelOrderTypeID { get; set; }

        public int ExpectedNumberOfDays { get; set; }

        [StringLength(100)]
        public string ReasonForTravel { get; set; }

        public int? CityStartId { get; set; }

        public int? CityEndId { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalCost { get; set; }

        public DateTime? DocumentDate { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }

        public virtual Driver Driver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuelInfo> FuelInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteInfo> RouteInfo { get; set; }

        public virtual TravelOrderType TravelOrderType { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
