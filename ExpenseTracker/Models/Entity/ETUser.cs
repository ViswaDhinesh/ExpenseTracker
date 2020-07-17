namespace ExpenseTracker.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ETUser")]
    public partial class ETUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserUniqueID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsTwoFactor { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsEnableAccess { get; set; }

        public bool? IsEnableFullAccess { get; set; }

        public bool Status { get; set; }

        [StringLength(10)]
        public string Otp { get; set; }

        public string AndroidID { get; set; }

        public string UserDataID { get; set; }

        public string UserDataID1 { get; set; }

        public string UserDataID2 { get; set; }

        public string UserDataID3 { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public long? RoleID { get; set; }
    }
}
