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
        ETUser User = new ETUser();
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
            ViewBag.Role = repUsers.getRole();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_add(ETUser User)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    if (repUsers.LogInNameIsExist(User.LoginName, 0))
                    {
                        ViewBag.messagealert = "LogInName already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else if (repUsers.EmailIsExist(User.Email, 0) || !Common.IsValidEmail(User.Email))
                    {
                        if (!Common.IsValidEmail(User.Email))
                            ViewBag.messagealert = "Please Enter Valid Email";
                        else
                            ViewBag.messagealert = "Email already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else if (repUsers.PhoneIsExist(User.Phone, 0))
                    {
                        ViewBag.messagealert = "Phone already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else
                    {
                        User.SourceOfCreation = "User Form";
                        User.Password = Common.EncryptPassword(User.Password);
                        User.UserID = repUsers.UserIdGeneration();
                        User.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        User.CreatedDate = DateTime.Now;
                        User.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        User.ModifiedDate = DateTime.Now;
                        dbEntities.ETUsers.Add(User);
                        dbEntities.SaveChanges();
                        if (User.UserID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
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
            User = new ETUser();
            User = repUsers.GetUser(Id);
            User.Password = Common.DecryptPassword(User.Password);
            ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            //repUsers.getTitle("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.ReportingUser = repUsers.getMappedReportingUser();
            ViewBag.Role = repUsers.getRole();
            
            //ViewBag.ReportingUserID = User.ReportingUser;
            //ViewBag.UserLevelID = User.UserLevel;
            //ViewBag.RoleID = User.RoleID;
            //ViewBag.MaritalStatusID = User.MaritalStatus;
            //ViewBag.GenderID = User.Gender;
            //ViewBag.TitleID = User.Title;
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_edit(long id, ETUser updateUser)
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
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.Role = repUsers.getRole();
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
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.Role = repUsers.getRole();
                    return View(User);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, id))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.UserLevel = repUsers.getDataValues("UserLevel", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.ReportingUser = repUsers.getMappedReportingUser();
                    ViewBag.Role = repUsers.getRole();
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
                    User.RoleID = updateUser.RoleID;
                    User.LoginName = updateUser.LoginName;
                    User.Password = Common.EncryptPassword(updateUser.Password);
                    //User.Password = updateUser.Password;
                    User.IsTwoFactor = updateUser.IsTwoFactor;
                    User.UserLevel = updateUser.UserLevel;
                    User.ReportingUser = updateUser.ReportingUser;
                    //User.IsOwner = updateUser.IsOwner;
                    //User.IsAdmin = updateUser.IsAdmin;
                    //User.IsManager = updateUser.IsManager;
                    User.IsActive = updateUser.IsActive;
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
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        #endregion

        #region User View
        public ActionResult User_view(long Id)
        {
            User = repUsers.GetUser(Id);
            User.Password = Common.DecryptPassword(User.Password);
            return View(User);
        }
        #endregion

        #region User Delete
        public bool UserDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                User = new ETUser();
                User = dbEntities.ETUsers.Where(x => x.UserID == id).SingleOrDefault();
                if (User != null)
                {
                    dbEntities.ETUsers.Remove(User);
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
            User = new ETUser();
            User = repUsers.GetUser(Userid);

            if (User != null)
            {
                if (status)
                {
                    User.IsActive = false;
                    User.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    User.ModifiedDate = DateTime.Now;
                }
                else
                {
                    User.IsActive = true;
                    User.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    User.ModifiedDate = DateTime.Now;
                }
                dbEntities.Entry(User).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion
    }
}