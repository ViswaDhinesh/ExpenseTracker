using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker
{
    public class Common
    {
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