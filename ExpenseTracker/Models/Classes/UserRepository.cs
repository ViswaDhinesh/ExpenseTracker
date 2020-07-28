using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker
{
    public class UserRepository
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETUser User = new ETUser();
        List<ETUser> Users = new List<ETUser>();
        ETRole Role = new ETRole();
        List<ETRole> Roles = new List<ETRole>();
        ETMenu Menu = new ETMenu();
        List<ETMenu> Menus = new List<ETMenu>();
        ETSubMenu SubMenu = new ETSubMenu();
        List<ETSubMenu> SubMenus = new List<ETSubMenu>();

        // User
        #region Get All User
        public List<ETUser> GetAllUser(string UserType, long UserID, List<long> MappedUser)
        {
            Users = new List<ETUser>();
            if (UserType.ToUpper() == "OWNER")
                Users = dbEntities.ETUsers.OrderByDescending(x => x.UserID).ToList();
            else if (UserType.ToUpper() == "ADMIN")
                Users = dbEntities.ETUsers.Where(x => MappedUser.Contains(UserID)).OrderByDescending(x => x.UserID).ToList();
            else
                Users = dbEntities.ETUsers.Where(x => x.UserID == UserID).OrderByDescending(x => x.UserID).ToList();
            return Users;
        }
        #endregion

        #region Get User Exits
        public bool LogInNameIsExist(string LoginName, long UserId)
        {
            if (UserId != 0 && !string.IsNullOrEmpty(LoginName))
            {
                return dbEntities.ETUsers.Any(x => x.LoginName.ToLower().Trim().Equals(LoginName.ToLower().Trim()) && x.UserID != UserId);
            }
            else if (!string.IsNullOrEmpty(LoginName))
            {
                return dbEntities.ETUsers.Any(x => x.LoginName.ToLower().Trim().Equals(LoginName.ToLower().Trim()));
            }
            return false;
        }

        public bool EmailIsExist(string Email, long UserId)
        {
            if (UserId != 0 && !string.IsNullOrEmpty(Email))
            {
                return dbEntities.ETUsers.Any(x => x.Email.ToLower().Trim().Equals(Email.ToLower().Trim()) && x.UserID != UserId);
            }
            else if (!string.IsNullOrEmpty(Email))
            {
                return dbEntities.ETUsers.Any(x => x.Email.ToLower().Trim().Equals(Email.ToLower().Trim()));
            }
            return false;
        }

        public bool PhoneIsExist(string Phone, long UserId)
        {
            if (UserId != 0 && !string.IsNullOrEmpty(Phone))
            {
                return dbEntities.ETUsers.Any(x => x.Phone.ToLower().Trim().Equals(Phone.ToLower().Trim()) && x.UserID != UserId);
            }
            else if (!string.IsNullOrEmpty(Phone))
            {
                return dbEntities.ETUsers.Any(x => x.Phone.ToLower().Trim().Equals(Phone.ToLower().Trim()));
            }
            return false;
        }

        public long UserIdGeneration()
        {
            long UserId = RandomNumber(0, 9999);
            if (dbEntities.ETUsers.Any(x => x.UserID == UserId))
                UserIdGeneration();
            return UserId;
        }
        #endregion

        #region GetUser
        public ETUser GetUser(long UserId)
        {
            User = new ETUser();
            User = dbEntities.ETUsers.Where(x => x.UserID == UserId).Single();
            return User;
        }
        #endregion

        // User Common

        #region Common
        // Instantiate random number generator. 
        private readonly Random _random = new Random();
        // Generates a random number within a range.      
        public long RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        #endregion

        #region getDataValues
        public SelectList getDataValues(string Valuetype, string UserType, long UserID, long ReportingUserID)
        {
            IEnumerable<SelectListItem> DataValLst;
            if (UserType.ToUpper() == "OWNER")
                DataValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //(from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //else if (UserType.ToUpper() == "ADMIN")
            //    DataLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            else
                DataValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype && (m.UserID == null || m.UserID == 0 || m.UserID == UserID || m.UserID == ReportingUserID)) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            return new SelectList(DataValLst, "Value", "Text");
        }
        #endregion

        #region getTitle
        public SelectList getTitle(string Valuetype, string UserType, long UserID, long ReportingUserID)
        {
            IEnumerable<SelectListItem> RoleValLst;
            if (UserType.ToUpper() == "OWNER")
                RoleValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //(from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //else if (UserType.ToUpper() == "ADMIN")
            //    DataLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            else
                RoleValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype && (m.UserID == null || m.UserID == 0 || m.UserID == UserID || m.UserID == ReportingUserID)) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            return new SelectList(RoleValLst, "Value", "Text");
        }
        #endregion

        #region getRole
        public SelectList getRole()
        {
            IEnumerable<SelectListItem> RoleLst = (from m in dbEntities.ETRoles where m.IsActive == true select m).OrderBy(m => m.RoleID).AsEnumerable().Select(m => new SelectListItem() { Text = m.RoleName, Value = m.RoleID.ToString() });
            return new SelectList(RoleLst, "Value", "Text");
        }
        #endregion

        #region getMappedReportingUser
        public SelectList getMappedReportingUser()
        {
            IEnumerable<SelectListItem> MUserLst = (from m in dbEntities.ETUsers where (m.IsActive == true && (m.UserLevel == "ADMIN" || m.UserLevel == "OWNER")) select m).OrderBy(m => m.UserID).AsEnumerable().Select(m => new SelectListItem() { Text = m.FirstName, Value = m.UserID.ToString() });
            return new SelectList(MUserLst, "Value", "Text");
        }
        #endregion
        // Role
        #region GetRole
        public ETRole GetRole(long RoleId)
        {
            Role = new ETRole();
            Role = dbEntities.ETRoles.Where(x => x.RoleID == RoleId).Single();
            return Role;
        }
        #endregion

        #region Get All Role
        public List<ETRole> GetAllRole()
        {
            Roles = new List<ETRole>();
            Roles = dbEntities.ETRoles.OrderByDescending(x => x.RoleID).ToList();
            return Roles;
        }

        #endregion

        #region RoleIsExist
        public bool RoleIsExist(string RoleName, long RoleId)
        {
            if (RoleId != 0 && !string.IsNullOrEmpty(RoleName))
            {
                return dbEntities.ETRoles.Any(x => x.RoleName.ToLower().Trim().Equals(RoleName.ToLower().Trim()) && x.RoleID != RoleId);
            }
            else if (!string.IsNullOrEmpty(RoleName))
            {
                return dbEntities.ETRoles.Any(x => x.RoleName.ToLower().Trim().Equals(RoleName.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        // Menu
        #region GetMenu
        public ETMenu GetMenu(long MenuId)
        {
            Menu = new ETMenu();
            Menu = dbEntities.ETMenus.Where(x => x.MenuID == MenuId).Single();
            return Menu;
        }
        #endregion

        #region Get All Menu
        public List<ETMenu> GetAllMenu()
        {
            Menus = new List<ETMenu>();
            Menus = dbEntities.ETMenus.OrderByDescending(x => x.MenuID).ToList();
            return Menus;
        }
        #endregion

        #region MenuIsExist
        public bool MenuIsExist(string MenuName, long MenuId)
        {
            if (MenuId != 0 && !string.IsNullOrEmpty(MenuName))
            {
                return dbEntities.ETMenus.Any(x => x.MenuName.ToLower().Trim().Equals(MenuName.ToLower().Trim()) && x.MenuID != MenuId);
            }
            else if (!string.IsNullOrEmpty(MenuName))
            {
                return dbEntities.ETMenus.Any(x => x.MenuName.ToLower().Trim().Equals(MenuName.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        // Sub Menu
        #region GetSubMenu
        public ETSubMenu GetSubMenu(long SubMenuId)
        {
            SubMenu = new ETSubMenu();
            SubMenu = dbEntities.ETSubMenus.Where(x => x.SubMenuID == SubMenuId).Single();
            return SubMenu;
        }
        #endregion

        #region Get All SubMenu
        public List<ETSubMenu> GetAllSubMenu()
        {
            SubMenus = new List<ETSubMenu>();
            SubMenus = dbEntities.ETSubMenus.OrderByDescending(x => x.SubMenuID).ToList();
            return SubMenus;
        }
        #endregion

        #region SubMenuIsExist
        public bool SubMenuIsExist(string SubMenuName, long SubMenuId)
        {
            if (SubMenuId != 0 && !string.IsNullOrEmpty(SubMenuName))
            {
                return dbEntities.ETSubMenus.Any(x => x.SubMenuName.ToLower().Trim().Equals(SubMenuName.ToLower().Trim()) && x.SubMenuID != SubMenuId);
            }
            else if (!string.IsNullOrEmpty(SubMenuName))
            {
                return dbEntities.ETSubMenus.Any(x => x.SubMenuName.ToLower().Trim().Equals(SubMenuName.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        #region getMenuType
        public SelectList getMenuType()
        {
            IEnumerable<SelectListItem> StateLst = (from m in dbEntities.ETMenus where m.Status == true select m).OrderBy(m => m.MenuID).AsEnumerable().Select(m => new SelectListItem() { Text = m.MenuName, Value = m.MenuID.ToString() });
            return new SelectList(StateLst, "Value", "Text");
        }
        #endregion

        // Login

        #region GetUserEmail
        public ETUser GetUserEmail(string EmailId)
        {
            User = new ETUser();
            User = dbEntities.ETUsers.Where(x => x.Email == EmailId).SingleOrDefault();
            return User;
        }
        #endregion

        #region CheckIPAddress
        public bool CheckIPAddress(string email, bool isipcheck)
        {
            //if (isipcheck)
            //{
            //    string currentipaddress = GetIPAddress();
            //    return dbEntities.TBL_IP_MASTER.Any(x => x.IS_ACTIVE && x.IP_ADDRESS.Trim().Equals(currentipaddress.Trim()));
            //}
            return true;
        }
        #endregion

        #region CheckUserandPasswordIsValid
        public bool CheckUserIsValid(string email)
        {
            return dbEntities.ETUsers.Any(x => x.Email.Trim().Equals(email.Trim()));
        }

        public bool CheckPasswordIsValid(string email, string password)
        {
            //byte[] password_bt = repcommon.Encrypt(password, key, iv);
            password = Common.EncryptPassword(password);
            return dbEntities.ETUsers.Any(x => x.Email.Trim().Equals(email.Trim()) && x.Password == password && x.IsActive == true);
        }
        #endregion

        #region CheckLoginUser
        public LoginDetailCheck CheckLoginUser(LoginDetail objLoginDetails)
        {
            LoginDetailCheck logincheck = new LoginDetailCheck();
            if (dbEntities.ETUsers.Any(x => x.Email.Trim().Equals(objLoginDetails.Email.Trim()) && x.Password != null))
            {
                User = GetUserEmail(objLoginDetails.Email);
                if (CheckPasswordIsValid(objLoginDetails.Email, objLoginDetails.Password))
                {
                    logincheck.loginDetails = GetUserEmail(objLoginDetails.Email);
                    logincheck.isSuccess = true;
                    logincheck.errorMessage = "";
                    return logincheck;
                    //if (CheckIPAddress(objLoginDetails.Email, User.IsActive)) // Needs to change the ip address check
                    //{
                    //    logincheck.isSuccess = true;
                    //    logincheck.errorMessage = "";
                    //    logincheck.loginDetails = GetUserEmail(objLoginDetails.Email);
                    //    return logincheck;
                    //}
                    //else
                    //{
                    //    logincheck.isSuccess = false;
                    //    string currentipaddress = repcommon.GetIPAddress();
                    //    logincheck.errorMessage = "Permission denied. Cuurent IP:" + currentipaddress;
                    //    logincheck.loginDetails = null;
                    //    return logincheck;
                    //}
                }
            }
            logincheck.isSuccess = false;
            logincheck.errorMessage = objLoginDetails.GetTypes + " or password incorrect.";
            logincheck.loginDetails = null;
            return logincheck;
        }

        #endregion

        #region LogForUserLogin
        public void LogForUserLogin(LoginDetailCheck checkLogin, string email)
        {

            ETUserLog loginCapture = new ETUserLog();
            loginCapture.IPAddress = GetIPAddress();
            loginCapture.IsSuccess = Convert.ToBoolean(checkLogin.isSuccess ? 1 : 0);
            loginCapture.LoginDate = DateTime.Now;
            loginCapture.CreatedDate = DateTime.Now;
            loginCapture.ModifiedDate = DateTime.Now;
            loginCapture.CreatedBy = checkLogin.loginDetails.UserID.ToString();
            loginCapture.ModifiedBy = checkLogin.loginDetails.UserID.ToString();
            if (checkLogin.loginDetails != null)
            {
                loginCapture.UserID = checkLogin.loginDetails.UserID;
                loginCapture.LoginName = checkLogin.loginDetails.LoginName;
                loginCapture.Password = Common.EncryptPassword(checkLogin.loginDetails.Password);
                loginCapture.SessionID = checkLogin.loginDetails.UserID;
            }
            else
            {
                loginCapture.UserID = 0;
                loginCapture.LoginName = email;
                loginCapture.Password = null;
                loginCapture.SessionID = 0;
            }
            dbEntities.ETUserLogs.Add(loginCapture);
            dbEntities.SaveChanges();
        }
        #endregion

        #region GetIPAddress
        public string GetIPAddress()
        {
            string currentIpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            return currentIpAddress;
        }
        #endregion

        // Common User

        #region Get User Password Exits
        public bool GetUserPasswordExits (long UserId, string Password)
        {
            if (UserId != 0 && !string.IsNullOrEmpty(Password))
            {
                return dbEntities.ETUsers.Any(x => x.Password.Equals(Password) && x.UserID == UserId);
            }
            return false;
        }
        #endregion

        #region GetUserForPasswordChange
        public ETUser GetUserForPasswordChange(long UserId)
        {
            User = new ETUser();
            User = dbEntities.ETUsers.Where(x => x.UserID == UserId).Single();

            //    .Select(item => new ETUser
            //{
            //    FirstName = item.FirstName,
            //    LastName = item.LastName,
            //    Email = item.Email,
            //    LoginName = item.LoginName,
            //    OldPassword = item.OldPassword,
            //    Password = item.Password,
            //    ConfirmPassword = item.ConfirmPassword,
            //    ModifiedBy = item.ModifiedBy,
            //    ModifiedDate = item.ModifiedDate,
            //}).ToList().Where(x => x.UserID == UserId).Single();
            return User;
        }
        #endregion
    }
}