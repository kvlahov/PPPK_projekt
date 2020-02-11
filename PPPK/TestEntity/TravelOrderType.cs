namespace TestEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TravelOrderType")]
    public partial class TravelOrderType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelOrderType()
        {
            TravelOrder = new HashSet<TravelOrder>();
        }

        [Key]
        public int IDTravelOrderType { get; set; }

        [Required]
        [StringLength(30)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelOrder> TravelOrder { get; set; }
    }
}
