using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ExpenseTracker
{
    [Table("ETMenu")]
    public partial class ETMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long MenuID { get; set; }

        [Required(ErrorMessage = "The MenuName field is required")]
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

    [Table("ETMenuAccess")]
    public partial class ETMenuAccess
    {
        [Key]
        public long MenuAccessID { get; set; }

        public int RoleID { get; set; }

        public long SubMenuID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }

    [Table("ETSource")]
    public partial class ETSource
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long SourceID { get; set; }

        [Required(ErrorMessage = "The SourceName field is required")]
        [StringLength(250)]
        public string SourceName { get; set; }

        [Required(ErrorMessage = "The SourceType field is required")]
        [StringLength(1)]
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

    [Table("ETSubMenu")]
    public partial class ETSubMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long SubMenuID { get; set; }

        public long MenuID { get; set; }

        [Required(ErrorMessage = "The SubMenuName field is required")]
        [StringLength(100)]
        public string SubMenuName { get; set; }

        [Required(ErrorMessage = "The SubMenuUrl field is required")]
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

    [Table("ETUser")]
    public partial class ETUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserUniqueID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The UserName field is required")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        [StringLength(10)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
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

    [Table("ETExpenseMaster")]
    public partial class ETExpenseMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ExpenseID { get; set; }

        [Required(ErrorMessage = "The ExpenseName field is required")]
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

    [Table("ETCategory")]
    public partial class ETCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long CategoryID { get; set; }

        [Required(ErrorMessage = "The CategoryName field is required")]
        [StringLength(250)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "The CategoryTypeID field is required")]
        public long CategoryTypeID { get; set; }

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

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
