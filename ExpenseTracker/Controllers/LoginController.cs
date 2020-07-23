using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class LoginController : Controller
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        //LoginRepository replogin = new LoginRepository();
        ETUser user = new ETUser();
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
                List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == roleId).Select(x => x.SubMenuID).ToList();
                if (lstSubmenuId.Count > 0)
                {
                    ETSubMenu objSubMenu = dbEntities.ETSubMenus.Where(n => lstSubmenuId.Contains(n.SubMenuID)).OrderBy(x => x.OrderNo).FirstOrDefault();
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
                        Session["UserName"] = loginDetails.FirstName;
                        Session["RoleID"] = loginDetails.RoleID;
                        Session["RoleName"] = null; //loginDetails.ETRole.RoleName;
                        Session["UserLevel"] = loginDetails.UserLevel; // New
                        Session["ReportingUser"] = loginDetails.ReportingUser;
                        MappedUser = dbEntities.ETUsers.Where(x => x.ReportingUser == loginDetails.UserID || x.UserID == loginDetails.UserID).Select(x => x.UserID).Distinct().ToList();
                        Session["MappedUser"] = MappedUser;
                        Session.Timeout = 300;
                        repUser.LogForUserLogin(checkLogin, objLoginDetails.Email);
                        List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == loginDetails.RoleID).Select(x => x.SubMenuID).ToList();
                        if (lstSubmenuId.Count > 0)
                        {
                            ETSubMenu objSubMenu = dbEntities.ETSubMenus.Where(n => lstSubmenuId.Contains(n.SubMenuID)).OrderBy(x => x.OrderNo).FirstOrDefault();
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
                    //repUser.LogForUserLogin(checkLogin, objLoginDetails.Email);
                    ViewBag.Error = checkLogin.errorMessage;
                    return View();
                }
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
            user = new ETUser();
            user = dbEntities.ETUsers.Where(x => x.Email == mailId || x.LoginName == mailId).SingleOrDefault();
            if (user != null)
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

        //#region Change Password

        //[HttpGet]
        //public JsonResult Checkpassword(string Oldpassword)
        //{
        //    var isDuplicate = false;

        //    long userId = Convert.ToInt64(Session["UserID"]);
        //    user = replogin.GetUser(userId);
        //    byte[] oldpassword = repcommon.Encrypt(Oldpassword.Trim(), user.PASSWORD_KEY, user.PASSWORD_IV);
        //    if (repcommon.Equality(user.PASSWORD, oldpassword))
        //    {
        //        isDuplicate = true;
        //    }
        //    var jsonData = new { isDuplicate };

        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult Changepassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //public ActionResult ChangePassword(ChangePasswordModel changePassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        long id = 0;
        //        byte[] encrypted;
        //        byte[] KEY;
        //        byte[] IV;
        //        ViewBag.messagealert = string.Empty;
        //        long userId = Convert.ToInt64(Session["UserID"]);
        //        user = replogin.GetUser(userId);
        //        byte[] oldpassword = repcommon.Encrypt(changePassword.OldPassword.Trim(), user.PASSWORD_KEY, user.PASSWORD_IV);
        //        if (repcommon.Equality(user.PASSWORD, oldpassword))
        //        {
        //            using (AesManaged aes = new AesManaged())
        //            {
        //                KEY = aes.Key;
        //                IV = aes.IV;
        //                encrypted = repcommon.Encrypt(changePassword.NewPassword, KEY, IV);
        //            }
        //            TBL_ADMIN_USER updateuser = dbEntities.TBL_ADMIN_USER.Where(x => x.ADMIN_USER_ID == userId).Single();
        //            updateuser.PASSWORD = encrypted;
        //            updateuser.PASSWORD_KEY = KEY;
        //            updateuser.PASSWORD_IV = IV;
        //            updateuser.MODIFIED_BY = Convert.ToString(Session["UserName"]);
        //            updateuser.MODIFIED_DATE = DateTime.Now;
        //            dbEntities.SaveChanges();
        //            id = updateuser.ADMIN_USER_ID;
        //            bool mail = mailing.PasswordChanged(updateuser.EMAIL_ID, updateuser.NAME, "Your Password Changed", updateuser.LOGIN_NAME, "password");
        //            if (id != 0)
        //            {
        //                Session["UserID"] = null;
        //                Session["UserName"] = null;
        //                Session["RoleID"] = null;
        //                Session["RoleName"] = null;
        //                TempData["SessionExpired"] = "Password changed successfully";
        //                return RedirectToAction("Login");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("OldPassword", "The old password is incorrect");
        //        }
        //    }
        //    return View(changePassword);
        //}

        //#endregion  Change Password

    }
}