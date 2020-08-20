using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker
{
    public class LandRepository
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETLandDetails Land = new ETLandDetails();
        List<ETLandDetails> Lands = new List<ETLandDetails>();
        ETLandDetailsLog LandLog = new ETLandDetailsLog();
        List<ETLandDetailsLog> LandLogs = new List<ETLandDetailsLog>();

        #region Get All Land
        public List<ETLandDetails> GetAllLand(long UserID, string UserLevel)
        {
            Lands = new List<ETLandDetails>();
            if (UserLevel.ToUpper() == "OWNER")
                Lands = dbEntities.ETLandDetail.OrderByDescending(x => x.LandID).ToList();
            else
                Lands = dbEntities.ETLandDetail.Where(x => x.UserID == UserID).OrderByDescending(x => x.LandID).ToList();
            return Lands;
        }

        #endregion

        #region GetLand
        public ETLandDetails GetLand(long LandId)
        {
            Land = new ETLandDetails();
            Land = dbEntities.ETLandDetail.Where(x => x.LandID == LandId).Single();
            return Land;
        }
        #endregion

        #region getDataValues
        public SelectList getDataValues(string Valuetype, string UserType, long UserID, long ReportingUserID)
        {
            IEnumerable<SelectListItem> DataValLst;
            if (UserType.ToUpper() == "OWNER")
                DataValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.OrderNo).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //(from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            //else if (UserType.ToUpper() == "ADMIN")
            //    DataLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype) select m).OrderBy(m => m.ValueID).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            else
                DataValLst = (from m in dbEntities.ETValues where (m.IsActive == true && m.ValueType == Valuetype && (m.UserID == null || m.UserID == 0 || m.UserID == UserID || m.UserID == ReportingUserID)) select m).OrderBy(m => m.OrderNo).AsEnumerable().Select(m => new SelectListItem() { Text = m.ValueName, Value = m.ValueUniqueID.ToString() });
            return new SelectList(DataValLst, "Value", "Text");
        }
        #endregion
    }
}