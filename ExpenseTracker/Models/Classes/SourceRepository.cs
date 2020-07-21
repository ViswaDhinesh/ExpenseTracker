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
        public List<ETSource> GetAllSource()
        {
            Sources = new List<ETSource>();
            Sources = dbEntities.ETSources.OrderByDescending(x => x.SourceID).ToList();
            //Sources = dbEntities.ETSources.Where(x => x.SourceID == 1).OrderByDescending(x => x.SourceID).ToList();
            return Sources;
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
        public List<ETCategory> GetAllCategory(long UserID)
        {
            Categories = new List<ETCategory>();
            Categories = dbEntities.ETCategories.Where(x=> x.UserID == UserID).OrderByDescending(x => x.CategoryID).ToList();
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
    }
}