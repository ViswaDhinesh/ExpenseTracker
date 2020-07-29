using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETUser Userval = new ETUser();
        ETValue Values = new ETValue();
        UserRepository repUsers = new UserRepository();
        List<ETUser> Users = new List<ETUser>();

        #region User List
        public ActionResult Index()
        {
            long user = Convert.ToInt64(Session["UserID"]); // Need to change
            ViewBag.UserPermission = Session["UserLevel"].ToString().ToUpper();
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Users = new List<ETUser>();
            if (Session["UserLevel"].ToString().ToUpper() == "OWNER")
                Users = repUsers.GetAllUser("OWNER", 0, null);
            else if (Session["UserLevel"].ToString().ToUpper() == "ADMIN")
            {
                var UserList = Session["MappedUser"] as List<long>;
                Users = repUsers.GetAllUser("ADMIN", user, UserList);
            }
            else
                Users = repUsers.GetAllUser("USER", user, null);
            return View(Users);
        }
        #endregion

        #region User Add
        [HttpGet]
        public ActionResult User_add()
        {
            ViewBag.messagealert = string.Empty; // Need to change the get data values while admin access.
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.ReportingUser = repUsers.getMappedReportingUser();//getDataValues("ReportingUser", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"])); // Need to change
            ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Role = repUsers.getRole();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_add(ETUser addUser)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (addUser != null)
                {
                    if (repUsers.LogInNameIsExist(addUser.LoginName, 0))
                    {
                        ViewBag.messagealert = "LogInName already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Role = repUsers.getRole();
                        return View(addUser);
                    }
                    else if (repUsers.EmailIsExist(addUser.Email, 0) || !Common.IsValidEmail(addUser.Email))
                    {
                        if (!Common.IsValidEmail(addUser.Email))
                            ViewBag.messagealert = "Please Enter Valid Email";
                        else
                            ViewBag.messagealert = "Email already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Role = repUsers.getRole();
                        return View(addUser);
                    }
                    else if (repUsers.PhoneIsExist(addUser.Phone, 0))
                    {
                        ViewBag.messagealert = "Phone already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Role = repUsers.getRole();
                        return View(addUser);
                    }
                    else
                    {
                        //try
                        //{
                        addUser.SourceOfCreation = "User Form";
                        addUser.Password = Common.EncryptPassword(addUser.Password);
                        addUser.ConfirmPassword = Common.EncryptPassword(addUser.ConfirmPassword);
                        addUser.UserID = repUsers.UserIdGeneration();
                        addUser.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        addUser.CreatedDate = DateTime.Now;
                        addUser.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        addUser.ModifiedDate = DateTime.Now;
                        dbEntities.ETUsers.Add(addUser);
                        dbEntities.SaveChanges();

                        ETUserVerified userVerified = new ETUserVerified();
                        userVerified.UserID = addUser.UserID;
                        userVerified.IsEmailVefified = false;
                        userVerified.IsPhoneVerified = false;
                        userVerified.IsOtherVerified = false;
                        userVerified.IsOtherVerified1 = false;
                        userVerified.IsOtherVerified2 = false;
                        userVerified.IsOtherVerified3 = false;
                        userVerified.IsActive = true;
                        userVerified.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        userVerified.CreatedDate = DateTime.Now;
                        userVerified.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        userVerified.ModifiedDate = DateTime.Now;
                        dbEntities.ETUserVerifieds.Add(userVerified);
                        dbEntities.SaveChanges();

                        if (addUser.UserID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
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
                }
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        #endregion

        #region User Edit
        [HttpGet]
        public ActionResult User_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Userval = new ETUser();
            Userval = repUsers.GetUser(Id);
            Userval.Password = Common.DecryptPassword(Userval.Password);
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            //repUsers.getTitle("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.ReportingUser = repUsers.getMappedReportingUser();
            ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Role = repUsers.getRole();

            //ViewBag.ReportingUserID = Userval.ReportingUser;
            //ViewBag.UserLevelID = Userval.UserLevel;
            //ViewBag.RoleID = Userval.RoleID;
            //ViewBag.MaritalStatusID = Userval.MaritalStatus;
            //ViewBag.GenderID = Userval.Gender;
            //ViewBag.TitleID = Userval.Title;
            return View(Userval);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_edit(long id, ETUser updateUser)
        {
            TempData["messagealert"] = string.Empty;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                Userval = new ETUser();
                Userval = repUsers.GetUser(id);

                if (repUsers.LogInNameIsExist(updateUser.LoginName, id))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Role = repUsers.getRole();
                    return View(Userval);
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
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Role = repUsers.getRole();
                    return View(Userval);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, id))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Role = repUsers.getRole();
                    return View(Userval);
                }
                else
                {
                    long UserId = Convert.ToInt64(Session["UserID"]);
                    Userval.Title = updateUser.Title;
                    Userval.FirstName = updateUser.FirstName;
                    Userval.MiddleName = updateUser.MiddleName;
                    Userval.LastName = updateUser.LastName;
                    Userval.Email = updateUser.Email;
                    Userval.Phone = updateUser.Phone;
                    Userval.Gender = updateUser.Gender;
                    Userval.MaritalStatus = updateUser.MaritalStatus;
                    Userval.DOB = updateUser.DOB;
                    Userval.Address = updateUser.Address;
                    Userval.RoleID = updateUser.RoleID;
                    Userval.LoginName = updateUser.LoginName;
                    Userval.Password = Common.EncryptPassword(updateUser.Password);
                    Userval.ConfirmPassword = Common.EncryptPassword(updateUser.ConfirmPassword);
                    //Userval.Password = updateUser.Password;
                    Userval.IsTwoFactor = updateUser.IsTwoFactor;
                    Userval.UserLevel = updateUser.UserLevel;
                    Userval.ReportingUser = updateUser.ReportingUser;
                    //Userval.IsOwner = updateUser.IsOwner;
                    //Userval.IsAdmin = updateUser.IsAdmin;
                    //Userval.IsManager = updateUser.IsManager;
                    Userval.IsActive = updateUser.IsActive;
                    Userval.DeviceID = updateUser.DeviceID;
                    Userval.DeviceType = updateUser.DeviceType;
                    Userval.UserField1 = updateUser.UserField1;
                    Userval.UserField2 = updateUser.UserField2;
                    Userval.UserField3 = updateUser.UserField3;
                    Userval.ModifiedBy = UserId;
                    Userval.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Userval).State = EntityState.Modified;
                    dbEntities.SaveChanges();

                    var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == Userval.UserID && x.IsActive).FirstOrDefault();
                    if (userVerify == null)
                    {
                        ETUserVerified userVerified = new ETUserVerified();
                        userVerified.UserID = Userval.UserID;
                        userVerified.IsEmailVefified = false;
                        userVerified.IsPhoneVerified = false;
                        userVerified.IsOtherVerified = false;
                        userVerified.IsOtherVerified1 = false;
                        userVerified.IsOtherVerified2 = false;
                        userVerified.IsOtherVerified3 = false;
                        userVerified.IsActive = true;
                        userVerified.CreatedBy = UserId;
                        userVerified.CreatedDate = DateTime.Now;
                        userVerified.ModifiedBy = UserId;
                        userVerified.ModifiedDate = DateTime.Now;
                        dbEntities.ETUserVerifieds.Add(userVerified);
                        dbEntities.SaveChanges();
                    }

                    if (Userval.UserID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        #endregion

        #region User View
        public ActionResult User_view(long Id)
        {
            Userval = repUsers.GetUser(Id);
            Userval.Password = Common.DecryptPassword(Userval.Password);
            return View(Userval);
        }
        #endregion

        #region User Delete
        public bool UserDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Userval = new ETUser();
                Userval = dbEntities.ETUsers.Where(x => x.UserID == id).SingleOrDefault();
                if (User != null)
                {
                    dbEntities.ETUsers.Remove(Userval);
                    dbEntities.SaveChanges();
                    return true;
                }
            }
            return false;

            //if (!dbEntities.TBL_ADMIN_USER.Where(x => x.ROLE_ID == id).Any())
            //{
            //    TempData["messagealert"] = Status.Delete;
            //    role = new TBL_ROLE();
            //    role = dbEntities.TBL_ROLE.Where(x => x.ROLE_ID == id && x.ROLE_NAME != "superadmin").SingleOrDefault();
            //    if (role != null)
            //    {
            //        dbEntities.TBL_ROLE.Remove(role);
            //        dbEntities.SaveChanges();
            //        return true;
            //    }
            //}
            //return false;
        }
        #endregion

        #region UserUpdateStatus
        public bool UserUpdateStatus(bool status, long Userid)
        {
            Userval = new ETUser();
            Userval = repUsers.GetUser(Userid);

            if (Userval != null)
            {
                //try
                //{
                Userval.ConfirmPassword = Userval.Password;
                if (status)
                {
                    Userval.IsActive = false;
                    Userval.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Userval.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Userval.IsActive = true;
                    Userval.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Userval.ModifiedDate = DateTime.Now;
                }
                dbEntities.Entry(Userval).State = EntityState.Modified;
                dbEntities.SaveChanges();
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
                return true;
            }

            return false;
        }
        #endregion

        #region OtpUpdateStatus
        public bool OtpUpdateStatus(long Userid, long Otp)
        {
            Userval = new ETUser();
            Userval = repUsers.GetUser(Userid);

            if (Userval != null)
            {
                Userval.ConfirmPassword = Userval.Password;
                Userval.Otp = Otp.ToString();
                Userval.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                Userval.ModifiedDate = DateTime.Now;
                dbEntities.Entry(Userval).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion
    }
}