namespace SampleCheckList5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [Key]
        [Column("Items ID")]
        public int Items_ID { get; set; }

        [Column("Project ID")]
        public int Project_ID { get; set; }

        [Column("Items Name")]
        [Required]
        [StringLength(20)]
        public string Items_Name { get; set; }

        public virtual Project Project { get; set; }
    }
}
