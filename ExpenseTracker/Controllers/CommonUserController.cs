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
        ETUser User = new ETUser();
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
            User = new ETUser();
            User = repUsers.GetUser(userId);
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(User);
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
                User = new ETUser();
                User = repUsers.GetUser(userId);

                if (repUsers.LogInNameIsExist(updateUser.LoginName, userId))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(User);
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
                    return View(User);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, userId))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(User);
                }
                else
                {
                    //try
                    //{
                    User.Title = updateUser.Title;
                    User.FirstName = updateUser.FirstName;
                    User.MiddleName = updateUser.MiddleName;
                    User.LastName = updateUser.LastName;
                    User.Email = updateUser.Email;
                    User.Phone = updateUser.Phone;
                    User.Gender = updateUser.Gender;
                    User.MaritalStatus = updateUser.MaritalStatus;
                    User.DOB = updateUser.DOB;
                    User.Address = updateUser.Address;
                    User.LoginName = updateUser.LoginName;
                    User.Password = User.Password;
                    User.ConfirmPassword = User.Password;
                    //User.UserLevel = User.UserLevel;
                    User.IsTwoFactor = updateUser.IsTwoFactor;
                    User.DeviceID = updateUser.DeviceID;
                    User.UserField1 = updateUser.UserField1;
                    User.UserField2 = updateUser.UserField2;
                    User.UserField3 = updateUser.UserField3;
                    User.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    User.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(User).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (User.UserID != 0)
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
            //ViewBag.messagealert = string.Empty;
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
                User = new ETUser();
                User = repUsers.GetUserForPasswordChange(userId);

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
                    User.Password = Common.EncryptPassword(updateUser.NewPassword);
                    User.ConfirmPassword = Common.EncryptPassword(updateUser.ConfirmPassword);
                    User.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    User.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(User).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    //bool mail = mailing.PasswordChanged(updateuser.EMAIL_ID, updateuser.NAME, "Your Password Changed", updateuser.LOGIN_NAME, "password");
                    if (User.UserID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        #endregion
    }
}