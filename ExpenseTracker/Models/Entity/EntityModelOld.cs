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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MenuID { get; set; }

        [Required(ErrorMessage = "The MenuName field is required")]
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

    public partial class ETSubMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    public partial class ETMenuAccess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MenuAccessID { get; set; }

        public long RoleID { get; set; }

        public long MenuID { get; set; }

        public long SubMenuID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }

    public partial class ETUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserUniqueID { get; set; }

        [Key]
        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The UserName field is required")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        [StringLength(10)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }

        public long RoleID { get; set; }

        [Required(ErrorMessage = "The IsTwoFactor field is required")]
        public bool IsTwoFactor { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsEnableAccess { get; set; }

        public bool? IsEnableFullAccess { get; set; }

        [Required(ErrorMessage = "The Status field is required")]
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
    }

    public partial class ETExpenseMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ExpenseID { get; set; }

        [Required(ErrorMessage = "The ExpenseName field is required")]
        [StringLength(100)]
        public string ExpenseName { get; set; }

        [Required(ErrorMessage = "The ExpenseType field is required")]
        public bool ExpenseType { get; set; }

        [Required(ErrorMessage = "The ExpenseStatus field is required")]
        public bool ExpenseStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
