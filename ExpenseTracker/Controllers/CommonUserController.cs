using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class CommonUserController : BaseController
    {
        // GET: CommonUser
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETUser UserVal = new ETUser();
        UserRepository repUsers = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        #region Profiles
        [HttpGet]
        public ActionResult Profiles()
        {
            ViewBag.messagealert = string.Empty;
            long userId = Convert.ToInt64(Session["UserID"]);
            UserVal = new ETUser();
            UserVal = repUsers.GetUser(userId);
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(UserVal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profiles(ProfileChange updateUser)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                long userId = Convert.ToInt64(Session["UserID"]);
                UserVal = new ETUser();
                UserVal = repUsers.GetUser(userId);

                if (repUsers.LogInNameIsExist(updateUser.LoginName, userId))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(UserVal);
                }
                else if (repUsers.EmailIsExist(updateUser.Email, userId) || !Common.IsValidEmail(updateUser.Email))
                {
                    if (!Common.IsValidEmail(updateUser.Email))
                        ViewBag.messagealert = "Please Enter Valid Email";
                    else
                        ViewBag.messagealert = "Email already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(UserVal);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, userId))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(UserVal);
                }
                else
                {
                    //try
                    //{
                    UserVal.Title = updateUser.Title;
                    UserVal.FirstName = updateUser.FirstName;
                    UserVal.MiddleName = updateUser.MiddleName;
                    UserVal.LastName = updateUser.LastName;
                    UserVal.Email = updateUser.Email;
                    UserVal.Phone = updateUser.Phone;
                    UserVal.Gender = updateUser.Gender;
                    UserVal.MaritalStatus = updateUser.MaritalStatus;
                    UserVal.DOB = updateUser.DOB;
                    UserVal.Address = updateUser.Address;
                    UserVal.LoginName = updateUser.LoginName;
                    //UserVal.Password = UserVal.Password;
                    UserVal.ConfirmPassword = UserVal.Password;
                    //UserVal.UserLevel = UserVal.UserLevel;
                    UserVal.IsTwoFactor = updateUser.IsTwoFactor;
                    UserVal.DeviceID = updateUser.DeviceID;
                    UserVal.UserField1 = updateUser.UserField1;
                    UserVal.UserField2 = updateUser.UserField2;
                    UserVal.UserField3 = updateUser.UserField3;
                    UserVal.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    UserVal.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(UserVal).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (UserVal.UserID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                    //}
                    //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    //{
                    //    Exception raise = dbEx;
                    //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //    {
                    //        foreach (var validationError in validationErrors.ValidationErrors)
                    //        {
                    //            string message = string.Format("{0}:{1}",
                    //                validationErrors.Entry.Entity.ToString(),
                    //                validationError.ErrorMessage);
                    //            // raise a new exception nesting
                    //            // the current instance as InnerException
                    //            raise = new InvalidOperationException(message, raise);
                    //        }
                    //    }
                    //    throw raise;
                    //}
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion

        #region PasswordChange
        [HttpGet]
        public ActionResult PasswordChange()//(long Id)
        {
            ViewBag.messagealert = string.Empty;
            //User = new ETUser();
            //User = repUsers.GetUserForPasswordChange(Id);
            //User.Password = string.Empty;
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult PasswordChange(PasswordChange updateUser)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                long userId = Convert.ToInt64(Session["UserID"]);
                UserVal = new ETUser();
                UserVal = repUsers.GetUserForPasswordChange(userId);

                if (!repUsers.GetUserPasswordExits(userId, Common.EncryptPassword(updateUser.OldPassword)))
                {
                    ViewBag.messagealert = "Please check your Old Password";
                    return View(updateUser);
                }
                else if (!updateUser.NewPassword.Equals(updateUser.ConfirmPassword))
                {
                    ViewBag.messagealert = "Please New and confirm password not match";
                    return View(updateUser);
                }
                else
                {
                    UserVal.Password = Common.EncryptPassword(updateUser.NewPassword);
                    UserVal.ConfirmPassword = Common.EncryptPassword(updateUser.ConfirmPassword);
                    UserVal.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    UserVal.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(UserVal).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    //bool mail = mailing.PasswordChanged(updateuser.EMAIL_ID, updateuser.NAME, "Your Password Changed", updateuser.LOGIN_NAME, "password");
                    if (UserVal.UserID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Logout", "Login");
            }
            return View();
        }
        #endregion

        #region IsTwofactor
        public ActionResult Twofactor()
        {
            ViewBag.messagealert = string.Empty;
            return View();
        }
        #endregion

        #region TwofactorEmailVerification
        [HttpGet]
        public ActionResult TwofactorEmailVerification()
        {
            long OneTimePassword = repUsers.OtpGeneration();
            string EmailId = Session["Email"].ToString();
            bool IsOtpUpdate = OtpUpdateStatus(Convert.ToInt64(Session["UserID"]), OneTimePassword);
            bool IsEmailSent = false;
            if (IsOtpUpdate)
            {
                IsEmailSent = true; // Need to change
            }
            if (!IsEmailSent)
            {
                ViewBag.messagealert = "Some problem in email. Please try again!";
                return RedirectToAction("Twofactor", "CommonUser");
            }
            else
            {
                ViewBag.messagealert = "OTP send it your Registered Email ID";
                if (Request.QueryString["OtpMode"] != null) { ViewBag.messagealert = "OTP again send it your Registered Email ID"; }
                return View();
            }
        }

        [HttpPost]
        public ActionResult TwofactorEmailVerification(TwoFactorVerification UserOtp)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            bool IsSuccess = OtpStatusVerify(Convert.ToInt64(Session["UserID"]), UserOtp.Otp);
            if (IsSuccess)
            {
                long UserId = Convert.ToInt64(Session["UserID"]);
                var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == UserId && x.IsActive && !x.IsEmailVefified).SingleOrDefault();
                if (userVerify != null)
                {
                    userVerify.IsEmailVefified = true;
                    userVerify.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    userVerify.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(userVerify).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                }
                long RoleID = Convert.ToInt64(Session["RoleID"]);
                List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == RoleID && n.Status).Select(x => x.SubMenuID).ToList();
                if (lstSubmenuId.Count > 0)
                {
                    ETSubMenu objSubMenu = dbEntities.ETSubMenus.Where(n => lstSubmenuId.Contains(n.SubMenuID) && n.Status && n.IsMainMenu).OrderBy(x => x.OrderNo).FirstOrDefault();
                    string Url = objSubMenu.SubMenuUrl;
                    if (!string.IsNullOrEmpty(Url))
                    {
                        string[] urls = Url.Split('/');
                        if (urls[1] != "" && urls[2] != "")
                        {
                            return RedirectToAction(urls[2], urls[1]);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
            else
            {
                ViewBag.messagealert = "Invalid OTP. Please Enter correct OTP and try again.!";
                return View();
            }
            ViewBag.messagealert = "Invalid OTP. Some problem in Verification. Please Try after some time.!";
            return View();
        }
        #endregion

        #region TwofactorPhoneVerification
        public ActionResult TwofactorPhoneVerification()
        {
            ViewBag.messagealert = string.Empty;
            return View();
        }
        #endregion

        #region TwofactorPhoneTabVerification
        public ActionResult TwofactorPhoneTabVerification()
        {
            ViewBag.messagealert = string.Empty;
            return View();
        }
        #endregion

        #region OtpUpdateStatus
        public bool OtpUpdateStatus(long Userid, long Otp)
        {
            UserVal = new ETUser();
            UserVal = repUsers.GetUser(Userid);

            if (UserVal != null)
            {
                UserVal.ConfirmPassword = UserVal.Password;
                UserVal.Otp = Otp.ToString();
                UserVal.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                UserVal.ModifiedDate = DateTime.Now;
                dbEntities.Entry(UserVal).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion

        #region OtpStatusVerify
        public bool OtpStatusVerify(long UserId, string Otp)
        {
            UserVal = new ETUser();
            UserVal = dbEntities.ETUsers.Where(x => x.UserID == UserId && x.Otp.ToString().Trim().Equals(Otp.Trim())).SingleOrDefault();
            if (UserVal != null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}