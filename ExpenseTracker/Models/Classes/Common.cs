using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ExpenseTracker
{
    public class Common
    {
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