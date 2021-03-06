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

        [Required(ErrorMessage = "The OrderNo field is required")]
        public long OrderNo { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }
    }

    [Table("ETMenuAccess")]
    public partial class ETMenuAccess
    {
        [Key]
        public long MenuAccessID { get; set; }

        public int RoleID { get; set; }

        public long MenuID { get; set; }

        public long SubMenuID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        [NotMapped]
        public List<ETSubMenu> lstsubmenu { get; set; }

        [NotMapped]
        public List<ETMenu> lstmenu { get; set; }

        [NotMapped]
        public List<ETMenuAccess> lstrolemenumap { get; set; }
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

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }
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

        [Required(ErrorMessage = "The OrderNo field is required")]
        public long OrderNo { get; set; }

        public bool Status { get; set; }

        public bool IsMainMenu { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        //public virtual ETMenu ETMenu { get; set; }

        [NotMapped]
        public string MenuName { get; set; }
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

        [Required(ErrorMessage = "The SourceID field is required")]
        public long SourceID { get; set; }

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        //public virtual ETSource ETSource { get; set; }

        [NotMapped]
        public string SourceName { get; set; }
    }

    [Table("ETValue")]
    public partial class ETValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ValueID { get; set; }

        [Required(ErrorMessage = "The Value UniqueID field is required")]
        [StringLength(20)]
        public string ValueUniqueID { get; set; }

        [Required(ErrorMessage = "The ValueName field is required")]
        [StringLength(100)]
        public string ValueName { get; set; }

        [Required(ErrorMessage = "The ValueType field is required")]
        [StringLength(100)]
        public string ValueType { get; set; }

        //[StringLength(20)]
        public long? UserID { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public long OrderNo { get; set; }
    }

    [Table("ETUser")]
    public partial class ETUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserUniqueID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The Title field is required")]
        [StringLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The FirstName field is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The LastName field is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Gender field is required")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The MaritalStatus field is required")]
        [StringLength(1)]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "The DOB field is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "The Address field is required")]
        [StringLength(250)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The RoleID field is required")]
        public long RoleID { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        [StringLength(10)]
        public string LoginName { get; set; }

        //[Required(ErrorMessage = "The Password field is required")]
        //public string Password { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "The confirm password field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The IsTwoFactor field is required")]
        public bool IsTwoFactor { get; set; }

        [Required(ErrorMessage = "The UserLevel field is required")]
        [StringLength(20)]
        public string UserLevel { get; set; }

        //[Required(ErrorMessage = "The SourceOfCreation field is required")]
        [StringLength(10)]
        public string SourceOfCreation { get; set; }

        public long? ReportingUser { get; set; }

        //public bool IsOwner { get; set; }

        //public bool IsAdmin { get; set; }

        //public bool IsManager { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        //public long? CreatedUserID { get; set; }

        [StringLength(10)]
        public string Otp { get; set; }

        public DateTime? OtpReceivedDate { get; set; }

        [StringLength(1)]
        public string OtpReceivedDevice { get; set; }

        public string DeviceID { get; set; }

        public string UserField1 { get; set; }

        public string UserField2 { get; set; }

        public string UserField3 { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "The CreatedBy field is required")]
        public long CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "The ModifiedBy field is required")]
        public long ModifiedBy { get; set; }

        [StringLength(10)]
        public string DeviceType { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Old Password is required.")]
        //public string OldPassword { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Confirmation Password is required.")]
        //[Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        //public string ConfirmPassword { get; set; }
    }

    [Table("ETUserVerified")]
    public partial class ETUserVerified
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long UserVerifyID { get; set; }

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The IsEmailVefified field is required")]
        public bool IsEmailVefified { get; set; }

        [Required(ErrorMessage = "The IsPhoneVerified field is required")]
        public bool IsPhoneVerified { get; set; }

        [Required(ErrorMessage = "The IsOtherVerified field is required")]
        public bool IsOtherVerified { get; set; }

        [Required(ErrorMessage = "The IsOtherVerified1 field is required")]
        public bool IsOtherVerified1 { get; set; }

        [Required(ErrorMessage = "The IsOtherVerified2 field is required")]
        public bool IsOtherVerified2 { get; set; }

        [Required(ErrorMessage = "The IsOtherVerified3 field is required")]
        public bool IsOtherVerified3 { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedBy { get; set; }
    }

    [NotMapped]
    public class PasswordChange
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string LoginName { get; set; }

        [Required(ErrorMessage = "The old password field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "The new password field is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "new password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The confirm password field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    [NotMapped]
    public class TwoFactorVerification
    {
        [Required(ErrorMessage = "The Otp field is required")]
        [StringLength(20)]
        public string Otp { get; set; }
    }

    [NotMapped]
    public partial class ProfileChange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The Title field is required")]
        [StringLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The FirstName field is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The LastName field is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Gender field is required")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The MaritalStatus field is required")]
        [StringLength(1)]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "The DOB field is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "The Address field is required")]
        [StringLength(250)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        [StringLength(10)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "The IsTwoFactor field is required")]
        public bool IsTwoFactor { get; set; }

        public string DeviceID { get; set; }

        public string UserField1 { get; set; }

        public string UserField2 { get; set; }

        public string UserField3 { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "The ModifiedBy field is required")]
        public long ModifiedBy { get; set; }

        [StringLength(10)]
        public string DeviceType { get; set; }
    }

    [NotMapped]
    public partial class SignUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The Title field is required")]
        [StringLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The FirstName field is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The LastName field is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Gender field is required")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The MaritalStatus field is required")]
        [StringLength(1)]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "The DOB field is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "The Address field is required")]
        [StringLength(250)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        [StringLength(10)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "The confirm password field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "The IsTwoFactor field is required")]
        //public bool IsTwoFactor { get; set; }

        public string DeviceID { get; set; }

        public string UserField1 { get; set; }

        public string UserField2 { get; set; }

        public string UserField3 { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "The ModifiedBy field is required")]
        public long ModifiedBy { get; set; }

        [StringLength(10)]
        public string DeviceType { get; set; }
    }

    [Table("ETRole")]
    public partial class ETRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long RoleID { get; set; }

        [Required(ErrorMessage = "The RoleName field is required")]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }
    }

    [Table("ETUserLog")]
    public partial class ETUserLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long UserLogID { get; set; }

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The LoginName field is required")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The IPAddress field is required")]
        [StringLength(100)]
        public string IPAddress { get; set; }

        public long SessionID { get; set; }

        [Required]
        public DateTime LoginDate { get; set; }

        [Required(ErrorMessage = "The IsSuccess field is required")]
        public bool IsSuccess { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }

    [NotMapped]
    public class LoginDetail
    {
        [Required(ErrorMessage = "The Email Id or Username field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }

        public string GetTypes { get; set; }
    }

    [NotMapped]
    public class LoginDetailCheck
    {
        public bool isSuccess { get; set; }

        public string errorMessage { get; set; }

        public ETUser loginDetails { get; set; }
    }

    // Bank

    [Table("ETBank")]
    public partial class ETBank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long BankID { get; set; }

        [Required(ErrorMessage = "The AccountHolder field is required")]
        [StringLength(100)]
        public string AccountHolder { get; set; }

        [Required(ErrorMessage = "The AccountNumber field is required")]
        [StringLength(100)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "The IFSCCode field is required")]
        [StringLength(50)]
        public string IFSCCode { get; set; }

        [Required(ErrorMessage = "The BankName field is required")]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required(ErrorMessage = "The BranchName field is required")]
        [StringLength(100)]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "The AccountType field is required")] //dropdown
        [StringLength(20)]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "The MinimumBalance field is required")]
        public decimal MinimumBalance { get; set; }

        public DateTime? AccountOpeningDate { get; set; }

        [StringLength(50)]
        public string CustomerID { get; set; }

        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(25)]
        public string ATMNumber { get; set; }

        [StringLength(10)]
        public string PINNumber { get; set; }

        public DateTime? ExpiredDate { get; set; }

        [StringLength(5)]
        public string CVV { get; set; }

        public bool IsCreditCard { get; set; }

        [Required(ErrorMessage = "The UserID field is required")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "The IsActive field is required")]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }
    }

    [Table("ETLandDetails")]
    public partial class ETLandDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long LandID { get; set; }
        public string District { get; set; }
        public string Taluk { get; set; }
        public string Division { get; set; }
        public string Panchayat { get; set; }
        public string Village { get; set; }
        public string OwnerType { get; set; }
        public string LandType { get; set; }
        public string LandArea { get; set; }
        public string PattaNumber { get; set; }
        public string PulaNumber { get; set; }
        public string SubDivisionNumber { get; set; }
        public string OldSubDivisionNumber { get; set; }
        public double? AcresSize { get; set; }
        public double? AresSize { get; set; }
        public double? HectareSize { get; set; }
        public string OwnerName { get; set; }
        public string OwnerNameInTamil { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsOwned { get; set; }
        public bool IsCommon { get; set; }
        public bool IsVerified { get; set; }
        public long UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedUser { get; set; }
    }

    [Table("ETLandDetailsLog")]
    public partial class ETLandDetailsLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long LandLogID { get; set; }
        public long LandID { get; set; }
        public string District { get; set; }
        public string Taluk { get; set; }
        public string Division { get; set; }
        public string Panchayat { get; set; }
        public string Village { get; set; }
        public string OwnerType { get; set; }
        public string LandType { get; set; }
        public string LandArea { get; set; }
        public string PattaNumber { get; set; }
        public string PulaNumber { get; set; }
        public string SubDivisionNumber { get; set; }
        public string OldSubDivisionNumber { get; set; }
        public double? AcresSize { get; set; }
        public double? AresSize { get; set; }
        public double? HectareSize { get; set; }
        public string OwnerName { get; set; }
        public string OwnerNameInTamil { get; set; }
        public string Remarks { get; set; }
        public string SourceType { get; set; }
        public bool IsActive { get; set; }
        public bool IsOwned { get; set; }
        public bool IsCommon { get; set; }
        public bool IsVerified { get; set; }
        public long UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedUser { get; set; }
    }
}
