using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class LoginController : Controller
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        //LoginRepository replogin = new LoginRepository();
        ETUser UserVal = new ETUser();
        UserRepository repUser = new UserRepository();

        //Mailing mailing = new Mailing();
        //CommonRepository repcommon = new CommonRepository();

        #region Login

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Error = string.Empty;
            ViewBag.messagealert = string.Empty;
            if (Session["UserID"] != null)
            {
                string roleName = Convert.ToString(Session["RoleName"]);
                long roleId = Convert.ToInt64(Session["RoleID"]);
                List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == roleId && n.Status).Select(x => x.SubMenuID).ToList();
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
            }
            else
            {
                ViewBag.messagealert = TempData["SessionExpired"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDetail objLoginDetails)
        {
            if (objLoginDetails != null)
            {
                if (Common.IsValidEmail(objLoginDetails.Email))
                {
                    objLoginDetails.GetTypes = "Email Id";
                }
                else
                {
                    objLoginDetails.GetTypes = "Usename";
                    if (dbEntities.ETUsers.Any(x => x.LoginName == objLoginDetails.Email))
                    {
                        objLoginDetails.Email = dbEntities.ETUsers.Where(x => x.LoginName == objLoginDetails.Email).Select(x => x.Email).First();
                    }
                }

                LoginDetailCheck checkLogin = repUser.CheckLoginUser(objLoginDetails);
                if (checkLogin.isSuccess)
                {
                    ETUser loginDetails = checkLogin.loginDetails;
                    if (loginDetails != null)
                    {
                        List<long> MappedUser = new List<long>();
                        Session["UserID"] = loginDetails.UserID;
                        Session["UserName"] = loginDetails.FirstName + " " + loginDetails.MiddleName + " " + loginDetails.LastName;
                        Session["RoleID"] = loginDetails.RoleID;
                        Session["RoleName"] = null; //loginDetails.ETRole.RoleName;
                        Session["LoginName"] = loginDetails.LoginName;  // New
                        Session["Email"] = loginDetails.Email;
                        Session["Phone"] = loginDetails.Phone;
                        Session["LastName"] = loginDetails.LastName;
                        Session["IsTwoFactor"] = loginDetails.IsTwoFactor;
                        Session["UserLevel"] = loginDetails.UserLevel;
                        Session["ReportingUser"] = loginDetails.ReportingUser;
                        MappedUser = dbEntities.ETUsers.Where(x => x.ReportingUser == loginDetails.UserID || x.UserID == loginDetails.UserID).Select(x => x.UserID).Distinct().ToList();
                        Session["MappedUser"] = MappedUser;
                        Session.Timeout = 300;
                        repUser.LogForUserLogin(checkLogin, objLoginDetails.Email);
                        var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == loginDetails.UserID && x.IsActive).FirstOrDefault();
                        if (loginDetails.IsTwoFactor)
                        {
                            return RedirectToAction("Twofactor", "CommonUser");
                        }
                        else if (userVerify != null && (!userVerify.IsEmailVefified || !userVerify.IsPhoneVerified)) // || !userVerify.IsOtherVerified))
                        {
                            if (!userVerify.IsEmailVefified)
                                //return RedirectToAction("TwofactorEmailVerification?VerifyMode=Email", "CommonUser");
                                return RedirectToAction("TwofactorEmailVerification", "CommonUser", new { VerifyMode = "Email" });
                            else if (!userVerify.IsPhoneVerified)
                                return RedirectToAction("TwofactorPhoneVerification", "CommonUser", new { VerifyMode = "Phone" });

                            //else if (!userVerify.IsOtherVerified) // Maybe verify this in future
                            //    return RedirectToAction("TwofactorPhoneTabVerification", "CommonUser", new { VerifyMode = "PhoneTab" });
                        }
                        else
                        {
                            List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == loginDetails.RoleID && n.Status).Select(x => x.SubMenuID).ToList();
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
                        }
                    }
                }
                else
                {
                    //repUser.LogForUserLogin(checkLogin, objLoginDetails.Email);
                    ViewBag.Error = checkLogin.errorMessage;
                    return View();
                }
            }
            return View();
        }

        #endregion

        #region DirectLogin
        public ActionResult DirectLogin()
        {
            if (Request.QueryString["RandomID"] != null)
            {
                LoginDetail objLoginDetails = new LoginDetail();
                objLoginDetails.Email = Common.DecryptPassword(Request.QueryString["RandomID"]);
                objLoginDetails.Password = Common.DecryptPassword(Request.QueryString["RandomValue"]);
                if (Common.IsValidEmail(objLoginDetails.Email))
                {
                    objLoginDetails.GetTypes = "Email Id";
                }
                else
                {
                    objLoginDetails.GetTypes = "Usename";
                    if (dbEntities.ETUsers.Any(x => x.LoginName == objLoginDetails.Email))
                    {
                        objLoginDetails.Email = dbEntities.ETUsers.Where(x => x.LoginName == objLoginDetails.Email).Select(x => x.Email).First();
                    }
                }

                LoginDetailCheck checkLogin = repUser.CheckLoginUserUsingOtp(objLoginDetails);
                if (checkLogin.isSuccess)
                {
                    ETUser loginDetails = checkLogin.loginDetails;
                    if (loginDetails != null)
                    {
                        List<long> MappedUser = new List<long>();
                        Session["UserID"] = loginDetails.UserID;
                        Session["UserName"] = loginDetails.FirstName + " " + loginDetails.MiddleName + " " + loginDetails.LastName;
                        Session["RoleID"] = loginDetails.RoleID;
                        Session["RoleName"] = null; //loginDetails.ETRole.RoleName;
                        Session["LoginName"] = loginDetails.LoginName;  // New
                        Session["Email"] = loginDetails.Email;
                        Session["Phone"] = loginDetails.Phone;
                        Session["LastName"] = loginDetails.LastName;
                        Session["IsTwoFactor"] = loginDetails.IsTwoFactor;
                        Session["UserLevel"] = loginDetails.UserLevel;
                        Session["ReportingUser"] = loginDetails.ReportingUser;
                        MappedUser = dbEntities.ETUsers.Where(x => x.ReportingUser == loginDetails.UserID || x.UserID == loginDetails.UserID).Select(x => x.UserID).Distinct().ToList();
                        Session["MappedUser"] = MappedUser;
                        Session.Timeout = 300;
                        repUser.LogForUserLogin(checkLogin, objLoginDetails.Email);
                        long UserId = Convert.ToInt64(Session["UserID"]);
                        var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == UserId && x.IsActive).FirstOrDefault();
                        if (userVerify != null && (!userVerify.IsEmailVefified || !userVerify.IsPhoneVerified))
                        {
                            if (Request.QueryString["VerifyMode"].ToString().Trim() == "Email")
                            {
                                userVerify.IsEmailVefified = true;
                            }
                            else if (Request.QueryString["VerifyMode"].ToString().Trim() == "Phone")
                            {
                                userVerify.IsPhoneVerified = true;
                            }
                            userVerify.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                            userVerify.ModifiedDate = DateTime.Now;
                            dbEntities.Entry(userVerify).State = EntityState.Modified;
                            dbEntities.SaveChanges();
                        }

                        List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == loginDetails.RoleID && n.Status).Select(x => x.SubMenuID).ToList();
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

                    }
                }
                else
                {
                    ViewBag.Error = checkLogin.errorMessage;
                    return View();
                }
            }
            return View();
        }
        #endregion

        #region SignUp
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.UserTitles = repUser.getDataValues("Title", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
            ViewBag.Gender = repUser.getDataValues("Gender", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
            ViewBag.MaritalStatus = repUser.getDataValues("MaritalStatus", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
            ViewBag.DeviceType = repUser.getDataValues("DeviceType", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUp updateUser)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                UserVal = new ETUser();

                if (repUser.LogInNameIsExist(updateUser.LoginName, 0))
                {
                    ViewBag.messagealert = "LogInName already exist";
                    ViewBag.UserTitles = repUser.getDataValues("Title", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.Gender = repUser.getDataValues("Gender", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.MaritalStatus = repUser.getDataValues("MaritalStatus", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.DeviceType = repUser.getDataValues("DeviceType", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    return View(UserVal);
                }
                else if (repUser.EmailIsExist(updateUser.Email, 0) || !Common.IsValidEmail(updateUser.Email))
                {
                    if (!Common.IsValidEmail(updateUser.Email))
                        ViewBag.messagealert = "Please Enter Valid Email";
                    else
                        ViewBag.messagealert = "Email already exist";
                    ViewBag.UserTitles = repUser.getDataValues("Title", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.Gender = repUser.getDataValues("Gender", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.MaritalStatus = repUser.getDataValues("MaritalStatus", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.DeviceType = repUser.getDataValues("DeviceType", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    return View(UserVal);
                }
                else if (repUser.PhoneIsExist(updateUser.Phone, 0))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUser.getDataValues("Title", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.Gender = repUser.getDataValues("Gender", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.MaritalStatus = repUser.getDataValues("MaritalStatus", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    ViewBag.DeviceType = repUser.getDataValues("DeviceType", "0", Convert.ToInt64(Session["UserID"]), 1); // Need to change
                    return View(UserVal);
                }
                else
                {
                    //try
                    //{
                    UserVal.UserID = repUser.UserIdGeneration();
                    UserVal.Password = Common.EncryptPassword(updateUser.Password);
                    UserVal.ConfirmPassword = Common.EncryptPassword(updateUser.ConfirmPassword);
                    UserVal.UserLevel = "USER";
                    UserVal.IsActive = true;
                    UserVal.IsTwoFactor = true;
                    UserVal.ReportingUser = 1; // Need to change
                    UserVal.RoleID = 3; // Need to change
                    UserVal.SourceOfCreation = "Sign Up";


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
                    //UserVal.IsTwoFactor = updateUser.IsTwoFactor;
                    UserVal.DeviceID = updateUser.DeviceID;
                    UserVal.DeviceType = updateUser.DeviceType;
                    UserVal.UserField1 = updateUser.UserField1;
                    UserVal.UserField2 = updateUser.UserField2;
                    UserVal.UserField3 = updateUser.UserField3;
                    UserVal.CreatedBy = UserVal.UserID;
                    UserVal.CreatedDate = DateTime.Now;
                    UserVal.ModifiedBy = UserVal.UserID;
                    UserVal.ModifiedDate = DateTime.Now;
                    dbEntities.ETUsers.Add(UserVal);
                    dbEntities.SaveChanges();
                    //dbEntities.Entry(UserVal).State = EntityState.Modified;
                    //dbEntities.SaveChanges();

                    ETUserVerified userVerified = new ETUserVerified();
                    userVerified.UserID = UserVal.UserID;
                    userVerified.IsEmailVefified = false;
                    userVerified.IsPhoneVerified = false;
                    userVerified.IsOtherVerified = false;
                    userVerified.IsOtherVerified1 = false;
                    userVerified.IsOtherVerified2 = false;
                    userVerified.IsOtherVerified3 = false;
                    userVerified.IsActive = true;
                    userVerified.CreatedBy = UserVal.UserID;
                    userVerified.CreatedDate = DateTime.Now;
                    userVerified.ModifiedBy = UserVal.UserID;
                    userVerified.ModifiedDate = DateTime.Now;
                    dbEntities.ETUserVerifieds.Add(userVerified);
                    dbEntities.SaveChanges();

                    if (UserVal.UserID != 0)
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
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        #endregion

        #region Logout

        public void Logout()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["RoleID"] = null;
            Session["RoleName"] = null;
            Session["LoginName"] = null;
            Session["Email"] = null;
            Session["Phone"] = null;
            Session["LastName"] = null;
            Session["IsTwoFactor"] = null;
            Session["UserLevel"] = null;
            Session["ReportingUser"] = null;
            Session["MappedUser"] = null;
            Response.Redirect("/Login");
        }

        #endregion

        #region Forgot Password

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public string ForgotPassword(string mailId)
        {
            UserVal = new ETUser();
            UserVal = dbEntities.ETUsers.Where(x => x.Email == mailId || x.LoginName == mailId).SingleOrDefault();
            if (UserVal != null)
            {
                //user.IS_CREATED = false;
                //user.RANDOM_ID = user.RANDOM_ID + 1;
                //dbEntities.SaveChanges();


                //bool isMailsent = mailing.AdminCreatePassword(user.Email, user.FirstName, repcommon.EncryptPassword(user.ADMIN_USER_ID.ToString()), repcommon.EncryptPassword(user.RANDOM_ID.ToString()), user.LoginName);
                //if (isMailsent)
                //{
                //    return "success";
                //}
            }
            else
            {
                return "notexist";
            }
            return "failed";
        }

        #endregion Forgot Password

        #region Error

        public ActionResult Error()
        {
            return View();
        }

        #endregion

        #region Expired

        public ActionResult Expired()
        {
            return View();
        }

        #endregion        

    }
}