namespace ExpenseTracker.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ETMenu")]
    public partial class ETMenu
    {
        [Key]
        public long MenuID { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        [StringLength(500)]
        public string MenuUrl { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
