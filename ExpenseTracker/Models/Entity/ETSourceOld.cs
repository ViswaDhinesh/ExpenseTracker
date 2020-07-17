using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ExpenseTracker
{
    public partial class ETSourceOld
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long SourceID { get; set; }

        [Required(ErrorMessage = "The SourceName field is required")]
        
        public string SourceName { get; set; }

        [Required(ErrorMessage = "The SourceType field is required")]
        public string SourceType { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}