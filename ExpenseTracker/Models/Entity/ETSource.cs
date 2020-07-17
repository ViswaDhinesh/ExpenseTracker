namespace ExpenseTracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ETSource")]
    public partial class ETSource
    {
        [Key]
        public long SourceID { get; set; }

        [Required]
        [StringLength(250)]
        public string SourceName { get; set; }

        [Required]
        [StringLength(1)]
        public string SourceType { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
