using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using Telesign;

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
            ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
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
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
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
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(UserVal);
                }
                else if (repUsers.PhoneIsExist(updateUser.Phone, userId))
                {
                    ViewBag.messagealert = "Phone already exist";
                    ViewBag.UserTitles = repUsers.getDataValues("Title", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Gender = repUsers.getDataValues("Gender", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.MaritalStatus = repUsers.getDataValues("MaritalStatus", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.DeviceType = repUsers.getDataValues("DeviceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
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
                    UserVal.DeviceType = updateUser.DeviceType;
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
            long UserID = Convert.ToInt64(Session["UserID"]);
            bool IsOtpUpdate = OtpUpdateStatus(UserID, OneTimePassword, "E");
            bool IsEmailSent = false;
            if (IsOtpUpdate)
            {
                string Title = "Two factor verification";
                if (Request.QueryString["VerifyMode"] != null)
                    Title = "Email verification";
                string LoginName = Session["LoginName"].ToString();
                var UserDet = dbEntities.ETUsers.Where(x => x.UserID == UserID && x.LoginName.Equals(LoginName)).SingleOrDefault();
                if (UserDet != null)
                {
                    //IsEmailSent = Common.EmailVerification(EmailId, UserName, LoginName, Otp, Title);
                    IsEmailSent = Common.EmailVerification(Title, UserDet, "Email");
                }
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
                if (Request.QueryString["VerifyMode"] != null)
                {
                    ViewBag.EnableSkip = "Enable";
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult TwofactorEmailVerification(TwoFactorVerification UserOtp)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string IsValid = OtpStatusVerify(Convert.ToInt64(Session["UserID"]), UserOtp.Otp, "E");
            if (IsValid == "Valid")
            {
                long UserId = Convert.ToInt64(Session["UserID"]);
                var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == UserId && x.IsActive && !x.IsEmailVefified).FirstOrDefault();
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
                    Session["IsVerifyTwofactor"] = "Y";
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
            else if (IsValid == "Otp")
            {
                ViewBag.messagealert = "Otp Expired. Please try again.!";
                return View();
            }
            else if (IsValid == "Device")
            {
                ViewBag.messagealert = "Your Device type is Invalid. Please try latest Device Link";
                return View();
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
        [HttpGet]
        public ActionResult TwofactorPhoneVerification()
        {
            long OneTimePassword = repUsers.OtpGeneration();
            long UserID = Convert.ToInt64(Session["UserID"]);
            bool IsOtpUpdate = OtpUpdateStatus(UserID, OneTimePassword, "P");
            ViewBag.messagealert = string.Empty;
            string IsSuccess = null;
            if (IsOtpUpdate)
            {
                string Title = "Two factor verification";
                if (Request.QueryString["VerifyMode"] != null)
                    Title = "Phone verification";
                string LoginName = Session["LoginName"].ToString();
                var UserDet = dbEntities.ETUsers.Where(x => x.UserID == UserID && x.LoginName.Equals(LoginName)).SingleOrDefault();
                if (UserDet != null)
                {
                    long ExpiredMinute = Convert.ToInt64(ConfigurationSettings.AppSettings["OtpValidTime"]);
                    string frontUrl = Convert.ToString(ConfigurationSettings.AppSettings["FrontURL"]);
                    string Urls = frontUrl + "Login/DirectLogin?RandomID=" + Common.EncryptPassword(UserDet.LoginName) + "&RandomValue=" + Common.EncryptPassword(UserDet.Otp) + "&VerifyMode=" + Common.EncryptPassword("Phone") + "";
                    string Messages = "Dear " + UserDet.FirstName + "\n" + " Your Otp is " + OneTimePassword + ". Your otp is expired within " +
                        ExpiredMinute + " minutes. Please Verify this asap. \n You can also use below direct link \n <a>" + Urls + "</a>";
                    //IsSuccess = sendSMStoPhone(Messages, "919629987977");
                    sendCalltoPhoneTel(Messages, "919629987977");
                    sendSMStoPhoneTel(Messages, "919629987977");
                }
                if (IsSuccess != null)
                //if (IsSuccess != null && IsSuccess.Contains("status:success"))
                {
                    ViewBag.messagealert = "Otp SMS sent to your registered phone number";
                    if (Request.QueryString["OtpMode"] != null) { ViewBag.messagealert = "OTP again send it your Registered Phone number"; }
                    if (Request.QueryString["VerifyMode"] != null)
                    {
                        ViewBag.EnableSkip = "Enable";
                    }
                    return View();
                }
                else
                {
                    ViewBag.messagealert = "Some problem in SMS. Please try again!";
                    return RedirectToAction("Twofactor", "CommonUser");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult TwofactorPhoneVerification(TwoFactorVerification UserOtp)
        {
            TempData["messagealert"] = string.Empty;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string IsValid = OtpStatusVerify(Convert.ToInt64(Session["UserID"]), UserOtp.Otp, "P");
            if (IsValid == "Valid")
            {
                long UserId = Convert.ToInt64(Session["UserID"]);
                var userVerify = dbEntities.ETUserVerifieds.Where(x => x.UserID == UserId && x.IsActive && !x.IsPhoneVerified).FirstOrDefault();
                if (userVerify != null)
                {
                    userVerify.IsPhoneVerified = true;
                    userVerify.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    userVerify.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(userVerify).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                }
                long RoleID = Convert.ToInt64(Session["RoleID"]);
                List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == RoleID && n.Status).Select(x => x.SubMenuID).ToList();
                if (lstSubmenuId.Count > 0)
                {
                    Session["IsVerifyTwofactor"] = "Y";
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
            else if (IsValid == "Otp")
            {
                ViewBag.messagealert = "Otp Expired. Please try again.!";
                return View();
            }
            else if (IsValid == "Device")
            {
                ViewBag.messagealert = "Your Device type is Invalid. Please try latest Device Link";
                return View();
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

        #region TwofactorPhoneTabVerification
        public ActionResult TwofactorPhoneTabVerification()
        {
            ViewBag.messagealert = string.Empty;
            PushNoti_Vendor_Add("A", "1");
            return View();
        }
        #endregion

        #region OtpUpdateStatus
        public bool OtpUpdateStatus(long Userid, long Otp, string DeviceType)
        {
            UserVal = new ETUser();
            UserVal = repUsers.GetUser(Userid);

            if (UserVal != null)
            {
                UserVal.ConfirmPassword = UserVal.Password;
                UserVal.Otp = Otp.ToString();
                UserVal.OtpReceivedDate = DateTime.Now.AddMinutes(Convert.ToInt64(ConfigurationSettings.AppSettings["OtpValidTime"]));
                UserVal.OtpReceivedDevice = DeviceType;
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
        public string OtpStatusVerify(long UserId, string Otp, string DeviceType)
        {
            UserVal = new ETUser();
            UserVal = dbEntities.ETUsers.Where(x => x.UserID == UserId && x.Otp.ToString().Trim().Equals(Otp.Trim()) && x.IsActive == true).SingleOrDefault();
            if (UserVal != null)
            {
                if (UserVal.OtpReceivedDevice != DeviceType)
                    return "Device";
                else if (!(UserVal.OtpReceivedDate > DateTime.Now))
                    return "Otp";
                else
                    return "Valid";
            }
            return "Invalid";
        }
        #endregion

        #region SkipVerification
        //[HttpPost]
        //public ActionResult SkipVerification()
        public ActionResult SkipVerification()
        {
            long RoleID = Convert.ToInt64(Session["RoleID"]);
            List<long> lstSubmenuId = dbEntities.ETMenuAccesses.Where(n => n.RoleID == RoleID && n.Status).Select(x => x.SubMenuID).ToList();
            if (lstSubmenuId.Count > 0)
            {
                Session["IsVerifyTwofactor"] = "N";
                ETSubMenu objSubMenu = dbEntities.ETSubMenus.Where(n => lstSubmenuId.Contains(n.SubMenuID) && n.Status && n.IsMainMenu).OrderBy(x => x.OrderNo).FirstOrDefault();
                string Url = objSubMenu.SubMenuUrl;
                if (!string.IsNullOrEmpty(Url))
                {
                    string[] urls = Url.Split('/');
                    if (urls[1] != "" && urls[2] != "")
                    {
                        return RedirectToAction(urls[2], urls[1]);
                        //return Response.Redirect("/Login");
                    }
                }
            }
            ViewBag.messagealert = "You don't have access!.";
            return RedirectToAction("Logout", "Login");
        }
        #endregion

        #region PushNotification

        public void PushNoti_Vendor_Add(string DeviceType, string DeviceID)
        {
            try
            {
                //string DeviceID = string.Join("\",\"", FCMToken);
                string postData = string.Empty;

                string ApplicationID = Convert.ToString(ConfigurationSettings.AppSettings["ApplicationID"]);
                string SenderID = Convert.ToString(ConfigurationSettings.AppSettings["SenderID"]);

                //Title and Description
                var Description = "Description";
                var Title = "Title";

                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send"); tRequest.Method = "post";

                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SenderID));

                if (DeviceType == "A") //Android
                {
                    postData = "{\"to\":\"" + DeviceID + "\",\"collapse_key\":\"type_a\",\"data\": {  \"body\" : " + "\"Description\",\"title\" : " + "\"" + Title + "\", \"message\" : " + "\"" + Description + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"}}";
                }
                else if (DeviceType == "I") //ios
                {
                    postData = "{\"to\":\"" + DeviceID + "\",\"notification\": { \"title\" : \"AUTHENTICATOR\", \"body\" : " + "\"Approve Log in Envision request from  " + Session["Email"].ToString().ToLower() + "\"}, \"priority\" : " + "\"high\"}";
                }

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse(); dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);

                //Get response from GCM server    
                string sResponseFromServer = tReader.ReadToEnd();
                FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                tReader.Close(); dataStream.Close();
                tResponse.Close();
            }
            catch (Exception oex)
            {

            }
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MobileStatus()
        {
            return "test";
        }

        #endregion

        #region sendSMStoPhone
        public string sendSMStoPhone(string Message, string Mobile)
        {
            string ApiKey = Convert.ToString(ConfigurationSettings.AppSettings["SMSApiKey"]);
            string ApiURL = Convert.ToString(ConfigurationSettings.AppSettings["SMSApiURL"]);
            string Sender = Convert.ToString(ConfigurationSettings.AppSettings["SMSSender"]);
            //ApiKey = "ljgV4Z8dgEQ-txqQaCbjejjvvUDbZgt0tBcVYm1rEl";
            //ApiURL = "https://api.textlocal.in/send/";
            //Sender = "TXTLCL";
            Message = HttpUtility.UrlEncode(Message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues(ApiURL, new NameValueCollection()
                {
                {"apikey" , ApiKey},
                {"numbers" , Mobile},
                {"message" , Message},
                {"sender" , Sender}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
        #endregion

        #region sendSMStoPhoneTel
        public string sendSMStoPhoneTel(string Message, string Mobile)
        {
            string customerId = "F246C360-8CFD-443D-9227-D30134043B75";
            string apiKey = "BdcTq1dXhaCnhKENlxEXzhYWm8B1Du/0bYDXMR8Ex+7Q3M9fMkFXQXiLzC6EdFovA7yIg7MYxigCj0UkkkpZwg==";

            string phoneNumber = "919629987977";

            string verifyCode = "12345";
            string message = string.Format("Your code is {0}", verifyCode);
            string messageType = "OTP";

            try
            {
                MessagingClient messagingClient = new MessagingClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = messagingClient.Message(phoneNumber, message, messageType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Console.WriteLine("Press any key to quit.");
            //Console.ReadKey();
            return "";
        }
        #endregion

        #region sendCalltoPhoneTel
        public string sendCalltoPhoneTel(string Message, string Mobile)
        {
            string customerId = "F246C360-8CFD-443D-9227-D30134043B75";
            string apiKey = "BdcTq1dXhaCnhKENlxEXzhYWm8B1Du/0bYDXMR8Ex+7Q3M9fMkFXQXiLzC6EdFovA7yIg7MYxigCj0UkkkpZwg==";

            string phoneNumber = "919629987977";

            string verifyCode = "12345";
            string message = string.Format("Hello, your code is {0}. Once again, your code is {1}. Goodbye.", verifyCode, verifyCode);
            string messageType = "OTP";

            try
            {
                VoiceClient voiceClient = new VoiceClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = voiceClient.Call(phoneNumber, message, messageType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Console.WriteLine("Press any key to quit.");
            //Console.ReadKey();
            return "";
        }
        #endregion

        //#region GetNotificationStatus
        //[System.Web.Services.WebMethod]
        //public static string GetNotificationStatus()
        //{
        //    var Session_ID = HttpContext.Current.Session["ID"];

        //    if (Session_ID != "" && Session_ID != null)
        //    {
        //        string ID = HttpContext.Current.Session["ID"].ToString();
        //        List<User> companyListForUser = new List<User>();
        //        User objUserLogin = new User();
        //        User objUserNotification = new User();
        //        User return_objUserLogin = null;
        //        objUserLogin.ID = Convert.ToInt32(ID);

        //        return_objUserLogin = new UserLoginService().GetNotificationStatus(objUserLogin, "null");

        //        HttpContext.Current.Session["AuthSession"] = return_objUserLogin;
        //        if (string.IsNullOrEmpty(return_objUserLogin.NotificationStatus.ToString()))
        //        {
        //            return "2";
        //        }

        //        if (return_objUserLogin.NotificationStatus == 0)
        //        {
        //            objUserNotification = new UserLoginService().UpdateNotificationStatus(objUserLogin, "null");
        //        }

        //        else if (return_objUserLogin.NotificationStatus == 1)
        //        {
        //            if (return_objUserLogin.ISDefaultDashboard.Trim() == "S" && return_objUserLogin.CanViewSelfService.Trim() == "Yes")
        //            {
        //                if (string.IsNullOrEmpty(return_objUserLogin.EmployeeID))
        //                {

        //                }
        //                else
        //                {
        //                    HttpContext.Current.Session["Modules"] = "S";

        //                }
        //            }
        //            else if (return_objUserLogin.ISDefaultDashboard.Trim() == "H" && return_objUserLogin.CanViewHR.Trim() == "Yes")
        //            {
        //                if (string.IsNullOrEmpty(return_objUserLogin.EmployeeID))
        //                {

        //                }
        //                else
        //                {
        //                    HttpContext.Current.Session["Modules"] = "H";
        //                }
        //            }
        //            else if (return_objUserLogin.ISDefaultDashboard.Trim() == "L" && return_objUserLogin.CanViewLogistics.Trim() == "Yes")
        //            {
        //                HttpContext.Current.Session["Modules"] = "L";
        //            }
        //            else if (return_objUserLogin.ISDefaultDashboard.Trim() == "SD" && return_objUserLogin.CanViewSalesDashboard.Trim() == "Yes")
        //            {
        //                HttpContext.Current.Session["Modules"] = "SD";
        //            }

        //            objUserNotification = new UserLoginService().UpdateNotificationStatus(objUserLogin, "null");
        //        }

        //        return return_objUserLogin.NotificationStatus.ToString();
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}

        //#endregion
    }
}