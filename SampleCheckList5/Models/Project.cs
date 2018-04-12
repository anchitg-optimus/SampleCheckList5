namespace SampleCheckList5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("CheckList ID")]
        public int CheckList_ID { get; set; }

        [Column("CheckList Name")]
        [Required]
        [StringLength(20)]
        public string CheckList_Name { get; set; }

        [Column("CheckList Owner")]
        [Required]
        [StringLength(20)]
        public string CheckList_Owner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}
