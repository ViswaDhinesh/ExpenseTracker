using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class CommonUserController : Controller
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
        public ActionResult Profiles(long Id)
        {
            ViewBag.messagealert = string.Empty;
            User = new ETUser();
            User = repUsers.GetUser(Id);
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profiles(long id, ETUser updateUser)
        {
            TempData["messagealert"] = string.Empty;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                User = new ETUser();
                User = repUsers.GetUser(id);

                if (repUsers.LogInNameIsExist(updateUser.LoginName, id))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(User);
                }
                else if (repUsers.EmailIsExist(updateUser.Email, id) || !Common.IsValidEmail(updateUser.Email))
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
                else if (repUsers.PhoneIsExist(updateUser.Phone, id))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(User);
                }
                else
                {
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
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion

        #region PasswordChange
        public ActionResult PasswordChange()
        {
            return View();
        }
        #endregion
    }
}