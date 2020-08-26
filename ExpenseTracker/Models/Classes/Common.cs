using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

namespace ExpenseTracker
{
    public class Common
    {
        public static int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["LPort"]);
        public static string siteUrl = Convert.ToString(ConfigurationSettings.AppSettings["SiteURL"]);
        public static string frontUrl = Convert.ToString(ConfigurationSettings.AppSettings["FrontURL"]);
        public static string hostAddress = Convert.ToString(ConfigurationSettings.AppSettings["LHost"]);
        //public string ToAdmin = Convert.ToString(ConfigurationSettings.AppSettings["ToAdminMailId"]);
        public static string fromMail = Convert.ToString(ConfigurationSettings.AppSettings["LMailId"]);
        public static string smtpLogin = Convert.ToString(ConfigurationSettings.AppSettings["LMailId"]);
        public static string smtpPassword = Convert.ToString(ConfigurationSettings.AppSettings["LMailPassword"]);
        public static bool sslId = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSLID"]);

        //public static bool EmailVerification(string EmailId, string UserName, string LoginName, string Otp, string Title)
        public static bool EmailVerification(string Title, ETUser UserDet, string VerifyMode)
        {
            try
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(hostAddress, portNumber);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = sslId;
                smtp.Credentials = new System.Net.NetworkCredential(smtpLogin, smtpPassword);

                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(fromMail, "Creative");
                mailMessage.To.Add(new System.Net.Mail.MailAddress(UserDet.Email));
                mailMessage.Subject = Title;
                mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                mailMessage.IsBodyHtml = true;

                string message_Body = string.Empty;
                string fPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Emails/EmailVerification.html");
                string logo = frontUrl + "Content/Admin/Images/Logo.jpg";
                StreamReader reader = new StreamReader(fPath);
                message_Body = reader.ReadToEnd();
                reader.Close();

                string DirectLogin = "Login/DirectLogin?RandomID=" + Common.EncryptPassword(UserDet.LoginName) + "&RandomValue=" + Common.EncryptPassword(UserDet.Otp) + "&VerifyMode=" + Common.EncryptPassword(VerifyMode) + "";
                message_Body = message_Body.Replace("@imgUrl@", logo);
                message_Body = message_Body.Replace("@UserName@", UserDet.FirstName + " " + UserDet.LastName);
                message_Body = message_Body.Replace("@LoginName@", UserDet.LoginName);
                message_Body = message_Body.Replace("@Otp@", UserDet.Otp);
                message_Body = message_Body.Replace("@SiteURL@", siteUrl);
                message_Body = message_Body.Replace("@year@", DateTime.Now.Year.ToString());
                message_Body = message_Body.Replace("@DirectLogin@", frontUrl + DirectLogin);
                message_Body = message_Body.Replace("@Details@", Title);
                message_Body = message_Body.Replace("@email@", UserDet.Email);

                mailMessage.Body = message_Body;

                Thread t1 = new Thread(delegate ()
                {
                    smtp.Send(mailMessage);
                });

                t1.Start();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Encrypt and Decrypt
        public static string DecryptPassword(string sPassword)
        {
            Byte[] dEC_data = Convert.FromBase64String(sPassword);
            string dEC_Str = ASCIIEncoding.ASCII.GetString(dEC_data);
            return dEC_Str;
        }
        public static string EncryptPassword(string sPassword)
        {
            SHA1Managed shaM = new SHA1Managed();
            Convert.ToBase64String(shaM.ComputeHash(Encoding.ASCII.GetBytes(sPassword)));
            byte[] eNC_data = ASCIIEncoding.ASCII.GetBytes(sPassword);
            string eNC_str = Convert.ToBase64String(eNC_data);
            return eNC_str;
        }
        #endregion

        #region IsValidEmail
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion
    }
    public sealed class Status
    {
        private Status() { }
        public const string Save = "Record added successfully";
        public const string Update = "Record updated successfully";
        public const string Delete = "Record deleted successfully";
        public const string Exist = "Record Already Exist";
        public const string Upload = "Please upload .csv files only";
    }
}