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
        long user = 1; // Need to change
        #region User List
        public ActionResult Index()
        {
            var IsPermission = dbEntities.ETUsers.Where(x => x.IsActive == true && x.UserID == user).SingleOrDefault(); // Need to change
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Users = new List<ETUser>();
            if (IsPermission == null || (!IsPermission.IsAdmin && !IsPermission.IsOwner))
                return RedirectToAction("Index", "Home");
            else if (IsPermission.IsOwner)
                Users = repUsers.GetAllUser("Owner", 0);
            else
                Users = repUsers.GetAllUser("", user); // Need to change
            return View(Users);
        }
        #endregion

        #region User Add
        [HttpGet]
        public ActionResult User_add()
        {
            //var IsPermission = dbEntities.ETUsers.Where(x => x.IsActive == true && x.UserID == user).SingleOrDefault(); // Need to change
            //if (IsPermission == null || (!IsPermission.IsAdmin && !IsPermission.IsOwner))
            //    return RedirectToAction("Index", "Home");
            //else
            //{
                ViewBag.messagealert = string.Empty;
                ViewBag.UserTitles = repUsers.getDataValues("Title");
                ViewBag.Gender = repUsers.getDataValues("Gender");
                ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                ViewBag.Role = repUsers.getRole();
                return View();
            //}
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
                        ViewBag.UserTitles = repUsers.getDataValues("Title");
                        ViewBag.Gender = repUsers.getDataValues("Gender");
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else if (repUsers.EmailIsExist(User.Email, 0))
                    {
                        ViewBag.messagealert = "Email already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title");
                        ViewBag.Gender = repUsers.getDataValues("Gender");
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else if (repUsers.PhoneIsExist(User.Phone, 0))
                    {
                        ViewBag.messagealert = "Phone already exist";
                        ViewBag.UserTitles = repUsers.getDataValues("Title");
                        ViewBag.Gender = repUsers.getDataValues("Gender");
                        ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                        ViewBag.Role = repUsers.getRole();
                        return View(User);
                    }
                    else
                    {
                        User.Password = Common.EncryptPassword(User.Password);
                        User.UserID = repUsers.UserIdGeneration();
                        User.CreatedBy = "Dinesh";//Session["UserName"].ToString();
                        User.CreatedDate = DateTime.Now;
                        User.ModifiedBy = "Pandiyan";//Session["UserName"].ToString();
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
            //var IsPermission = dbEntities.ETUsers.Where(x => x.IsActive == true && x.UserID == user).SingleOrDefault(); // Need to change
            //if (IsPermission == null || (!IsPermission.IsAdmin && !IsPermission.IsOwner))
            //    return RedirectToAction("Index", "Home");
            //else
            //{
                ViewBag.messagealert = string.Empty;
                User = new ETUser();
                User = repUsers.GetUser(Id);
                ViewBag.UserTitles = repUsers.getDataValues("Title");
                ViewBag.Gender = repUsers.getDataValues("Gender");
                ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                ViewBag.Role = repUsers.getRole();
                return View(User);
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_edit(long id, ETUser updateUser)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                User = new ETUser();
                User = repUsers.GetUser(id);

                if (repUsers.LogInNameIsExist(updateUser.LoginName, id))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title");
                    ViewBag.Gender = repUsers.getDataValues("Gender");
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                    ViewBag.Role = repUsers.getRole();
                    return View(User);
                }
                else if (repUsers.EmailIsExist(updateUser.Email, id))
                {
                    ViewBag.messagealert = "Email already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title");
                    ViewBag.Gender = repUsers.getDataValues("Gender");
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
                    ViewBag.Role = repUsers.getRole();
                    return View(User);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, id))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title");
                    ViewBag.Gender = repUsers.getDataValues("Gender");
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus");
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
                    User.IsOwner = updateUser.IsOwner;
                    User.IsAdmin = updateUser.IsAdmin;
                    User.IsManager = updateUser.IsManager;
                    User.IsActive = updateUser.IsActive;
                    User.DeviceID = updateUser.DeviceID;
                    User.UserField1 = updateUser.UserField1;
                    User.UserField2 = updateUser.UserField2;
                    User.UserField3 = updateUser.UserField3;
                    User.ModifiedBy = "Dinesh"; //Session["UserName"].ToString();
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
            //var IsPermission = dbEntities.ETUsers.Where(x => x.IsActive == true && x.UserID == user).SingleOrDefault(); // Need to change
            //if (IsPermission == null || (!IsPermission.IsAdmin && !IsPermission.IsOwner))
            //    return RedirectToAction("Index", "Home");
            //else
            //{
                User = repUsers.GetUser(Id);
                return View(User);
            //}
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
                    User.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
                    User.ModifiedDate = DateTime.Now;
                }
                else
                {
                    User.IsActive = true;
                    User.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
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