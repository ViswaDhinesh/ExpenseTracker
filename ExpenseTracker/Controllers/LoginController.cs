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

    }
}