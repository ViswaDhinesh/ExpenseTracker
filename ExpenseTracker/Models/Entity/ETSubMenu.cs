namespace ExpenseTracker.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ETSubMenu")]
    public partial class ETSubMenu
    {
        [Key]
        public long SubMenuID { get; set; }

        public long MenuID { get; set; }

        [Required]
        [StringLength(100)]
        public string SubMenuName { get; set; }

        [Required]
        [StringLength(500)]
        public string SubMenuUrl { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
