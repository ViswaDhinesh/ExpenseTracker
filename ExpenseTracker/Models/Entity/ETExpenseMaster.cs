namespace ExpenseTracker.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ETExpenseMaster")]
    public partial class ETExpenseMaster
    {
        [Key]
        public long ExpenseID { get; set; }

        [Required]
        [StringLength(100)]
        public string ExpenseName { get; set; }

        public bool ExpenseType { get; set; }

        public bool ExpenseStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
