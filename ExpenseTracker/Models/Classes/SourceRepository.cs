using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker
{
    public class SourceRepository
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETSource Source = new ETSource();
        List<ETSource> Sources = new List<ETSource>();
        ETCategory Category = new ETCategory();
        List<ETCategory> Categories = new List<ETCategory>();
        ETBank Bank = new ETBank();
        List<ETBank> Banks = new List<ETBank>();

        // Source
        #region SourceIsExist
        public bool SourceIsExist(string SourceName, long SourceId)
        {
            if (SourceId != 0 && !string.IsNullOrEmpty(SourceName))
            {
                return dbEntities.ETSources.Any(x => x.SourceName.ToLower().Trim().Equals(SourceName.ToLower().Trim()) && x.SourceID != SourceId);
            }
            else if (!string.IsNullOrEmpty(SourceName))
            {
                return dbEntities.ETSources.Any(x => x.SourceName.ToLower().Trim().Equals(SourceName.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        #region GetSource
        public ETSource GetSource(long SourceId)
        {
            Source = new ETSource();
            Source = dbEntities.ETSources.Where(x => x.SourceID == SourceId).Single();
            return Source;
        }
        #endregion

        #region Get All Source
        public List<ETSource> GetAllSource(long UserID, string UserLevel)
        {
            Sources = new List<ETSource>();
            if (UserLevel.ToUpper() == "OWNER")
                Sources = dbEntities.ETSources.OrderByDescending(x => x.SourceID).ToList();
            else
                Sources = dbEntities.ETSources.Where(x => x.UserID == UserID).OrderByDescending(x => x.SourceID).ToList();
            //Sources = dbEntities.ETSources.Where(x => x.SourceID == 1).OrderByDescending(x => x.SourceID).ToList();
            return Sources;
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

        // Category
        #region CategoryIsExist
        public bool CategoryIsExist(string CategoryName, long CategoryId)
        {
            if (CategoryId != 0 && !string.IsNullOrEmpty(CategoryName))
            {
                return dbEntities.ETCategories.Any(x => x.CategoryName.ToLower().Trim().Equals(CategoryName.ToLower().Trim()) && x.CategoryID != CategoryId);
            }
            else if (!string.IsNullOrEmpty(CategoryName))
            {
                return dbEntities.ETCategories.Any(x => x.CategoryName.ToLower().Trim().Equals(CategoryName.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        #region GetCategory
        public ETCategory GetCategory(long CategoryId)
        {
            Category = new ETCategory();
            Category = dbEntities.ETCategories.Where(x => x.CategoryID == CategoryId).Single();
            return Category;
        }
        #endregion

        #region Get All Category
        public List<ETCategory> GetAllCategory(long UserID, string UserLevel)
        {
            Categories = new List<ETCategory>();
            if (UserLevel.ToUpper() == "OWNER")
                Categories = dbEntities.ETCategories.OrderByDescending(x => x.CategoryID).ToList();
            else
                Categories = dbEntities.ETCategories.Where(x => x.UserID == UserID).OrderByDescending(x => x.CategoryID).ToList();
            //Categories = dbEntities.ETCategories.OrderByDescending(x => x.CategoryID).ToList();
            return Categories;
        }
        #endregion

        #region getSourceType
        public SelectList getSourceType()
        {
            IEnumerable<SelectListItem> StateLst = (from m in dbEntities.ETSources where m.IsActive == true select m).OrderBy(m => m.SourceID).AsEnumerable().Select(m => new SelectListItem() { Text = m.SourceName, Value = m.SourceID.ToString() });
            return new SelectList(StateLst, "Value", "Text");
        }
        #endregion

        // Bank

        #region Get All Bank
        public List<ETBank> GetAllBank(long UserID, string UserLevel)
        {
            Banks = new List<ETBank>();
            if (UserLevel.ToUpper() == "OWNER")
                Banks = dbEntities.ETBanks.OrderByDescending(x => x.BankID).ToList();
            else
                Banks = dbEntities.ETBanks.Where(x => x.UserID == UserID).OrderByDescending(x => x.BankID).ToList();
            return Banks;
        }

        #endregion

        #region AccountNumber IsExist
        //public bool AccountNumberIsExist(string AccountNumber)
        //{
        //    return dbEntities.ETBanks.Any(x => x.AccountNumber.ToLower().Trim().Equals(AccountNumber.ToLower().Trim()));
        //}

        public bool AccountNumberIsExist(string AccountNumber, long BankId)
        {
            if (BankId != 0 && !string.IsNullOrEmpty(AccountNumber))
            {
                return dbEntities.ETBanks.Any(x => x.AccountNumber.ToLower().Trim().Equals(AccountNumber.ToLower().Trim()) && x.BankID != BankId);
            }
            else if (!string.IsNullOrEmpty(AccountNumber))
            {
                return dbEntities.ETBanks.Any(x => x.AccountNumber.ToLower().Trim().Equals(AccountNumber.ToLower().Trim()));
            }
            return false;
        }

        #endregion

        #region CustomerID IsExist
        public bool CustomerIDIsExist(string CustomerID, long BankId)
        {
            if (BankId != 0 && !string.IsNullOrEmpty(CustomerID))
            {
                return dbEntities.ETBanks.Any(x => x.CustomerID.ToLower().Trim().Equals(CustomerID.ToLower().Trim()) && x.BankID != BankId);
            }
            else if (!string.IsNullOrEmpty(CustomerID))
            {
                return dbEntities.ETBanks.Any(x => x.CustomerID.ToLower().Trim().Equals(CustomerID.ToLower().Trim()));
            }
            return false;
        }
        #endregion

        #region GetBank
        public ETBank GetBank(long BankId)
        {
            Bank = new ETBank();
            Bank = dbEntities.ETBanks.Where(x => x.BankID == BankId).Single();
            return Bank;
        }
        #endregion
    }
}