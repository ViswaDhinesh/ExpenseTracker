using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class LandController : BaseController
    {
        // GET: Land
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        LandRepository repLand = new LandRepository();
        ETLandDetails Land = new ETLandDetails();
        List<ETLandDetails> Lands = new List<ETLandDetails>();
        ETLandDetailsLog LandLog = new ETLandDetailsLog();
        List<ETLandDetailsLog> LandLogs = new List<ETLandDetailsLog>();

        #region Land List
        public ActionResult Index()
        {
            ViewBag.UserPermission = Session["UserLevel"].ToString().ToUpper();
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Lands = new List<ETLandDetails>();
            Lands = repLand.GetAllLand(Convert.ToInt64(Session["UserID"]), Session["UserLevel"].ToString());
            return View(Lands);
        }
        #endregion

        #region Land Add

        [HttpGet]
        public ActionResult Land_Add()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.Districts = repLand.getDataValues("District", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Taluks = repLand.getDataValues("Taluk", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Divisions = repLand.getDataValues("Division", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Panchayats = repLand.getDataValues("Panchayat", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Villages = repLand.getDataValues("Village", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.OwnerTypes = repLand.getDataValues("OwnerType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.LandTypes = repLand.getDataValues("LandType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.LandAreas = repLand.getDataValues("LandArea", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Land_Add(ETLandDetails Land)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Land != null)
                {
                    //if (repLand.AccountNumberIsExist(Land.AccountNumber, 0))
                    //{
                    //    ViewBag.messagealert = "Account Number already exist";
                    //    ViewBag.AccountTypes = repLand.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    //    return View(Land);
                    //}
                    //else if (Land.CustomerID != null && Land.CustomerID != "" && repLand.CustomerIDIsExist(Land.CustomerID, 0))
                    //{
                    //    ViewBag.messagealert = "Customer ID already exist";
                    //    ViewBag.AccountTypes = repLand.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    //    return View(Land);
                    //}
                    //else if (repLand.LandEmailIsExist(Land.Email, Convert.ToInt64(Session["UserID"])) || !Common.IsValidEmail(Land.Email))
                    //{
                    //    if (!Common.IsValidEmail(Land.Email))
                    //        ViewBag.messagealert = "Please Enter Valid Email";
                    //    else
                    //        ViewBag.messagealert = "Email already exist";
                    //    ViewBag.AccountTypes = repLand.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    //    return View(Land);
                    //}
                    if (string.IsNullOrEmpty(Convert.ToString(Land.AresSize)) && string.IsNullOrEmpty(Convert.ToString(Land.HectareSize)) && string.IsNullOrEmpty(Convert.ToString(Land.AcresSize)))
                    {
                        ViewBag.messagealert = "Please Enter the Size of Land (Ares or Hectare or Acres).";
                        ViewBag.Districts = repLand.getDataValues("District", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Taluks = repLand.getDataValues("Taluk", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Divisions = repLand.getDataValues("Division", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Panchayats = repLand.getDataValues("Panchayat", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.Villages = repLand.getDataValues("Village", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.OwnerTypes = repLand.getDataValues("OwnerType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.LandTypes = repLand.getDataValues("LandType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        ViewBag.LandAreas = repLand.getDataValues("LandArea", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        return View(Land);
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(Convert.ToString(Land.AresSize)) && (string.IsNullOrEmpty(Convert.ToString(Land.HectareSize)) || string.IsNullOrEmpty(Convert.ToString(Land.AcresSize))))
                        {
                            Land.HectareSize = Land.AresSize / 100;
                            Land.AcresSize = (Convert.ToDouble(Land.AresSize) * 0.0247105);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(Land.HectareSize)) && (string.IsNullOrEmpty(Convert.ToString(Land.AresSize)) || string.IsNullOrEmpty(Convert.ToString(Land.AcresSize))))
                        {
                            Land.AresSize = Land.HectareSize * 100;
                            Land.AcresSize = (Convert.ToDouble(Land.HectareSize) * 2.47105);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(Land.AcresSize)) && (string.IsNullOrEmpty(Convert.ToString(Land.HectareSize)) || string.IsNullOrEmpty(Convert.ToString(Land.AresSize))))
                        {
                            Land.AresSize = (Convert.ToDouble(Land.AcresSize) / 0.0247105);
                            Land.HectareSize = (Convert.ToDouble(Land.AcresSize) / 2.47105);
                        }

                        Land.UserID = Convert.ToInt64(Session["UserID"]);
                        Land.CreatedUser = Convert.ToInt64(Session["UserID"]);
                        Land.CreatedDate = DateTime.Now;
                        Land.ModifiedUser = Convert.ToInt64(Session["UserID"]);
                        Land.ModifiedDate = DateTime.Now;
                        dbEntities.ETLandDetail.Add(Land);

                        // Land Log
                        ETLandDetailsLog LandLog = new ETLandDetailsLog();
                        LandLog.LandID = Land.LandID;
                        LandLog.District = Land.District;
                        LandLog.Taluk = Land.Taluk;
                        LandLog.Division = Land.Division;
                        LandLog.Panchayat = Land.Panchayat;
                        LandLog.Village = Land.Village;
                        LandLog.OwnerType = Land.OwnerType;
                        LandLog.LandType = Land.LandType;
                        LandLog.LandArea = Land.LandArea;
                        LandLog.PattaNumber = Land.PattaNumber;
                        LandLog.PulaNumber = Land.PulaNumber;
                        LandLog.SubDivisionNumber = Land.SubDivisionNumber;
                        LandLog.OldSubDivisionNumber = Land.OldSubDivisionNumber;
                        LandLog.AcresSize = Land.AcresSize;
                        LandLog.AresSize = Land.AresSize;
                        LandLog.HectareSize = Land.HectareSize;
                        LandLog.OwnerName = Land.OwnerName;
                        LandLog.OwnerNameInTamil = Land.OwnerNameInTamil;
                        LandLog.Remarks = Land.Remarks;
                        LandLog.SourceType = "C";
                        LandLog.IsActive = Land.IsActive;
                        LandLog.IsOwned = Land.IsOwned;
                        LandLog.IsCommon = Land.IsCommon;
                        LandLog.IsVerified = Land.IsVerified;
                        LandLog.UserID = Land.UserID;
                        LandLog.CreatedDate = Land.CreatedDate;
                        LandLog.CreatedUser = Land.CreatedUser;
                        LandLog.ModifiedDate = Land.ModifiedDate;
                        LandLog.ModifiedUser = Land.ModifiedUser;
                        dbEntities.ETLandDetailsLogs.Add(LandLog);

                        dbEntities.SaveChanges();
                        if (Land.LandID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Land");
            }
            return View();
        }
        #endregion

        #region Land Edit

        [HttpGet]
        public ActionResult Land_Edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Land = new ETLandDetails();
            Land = repLand.GetLand(Id);
            ViewBag.Districts = repLand.getDataValues("District", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Taluks = repLand.getDataValues("Taluk", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Divisions = repLand.getDataValues("Division", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Panchayats = repLand.getDataValues("Panchayat", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.Villages = repLand.getDataValues("Village", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.OwnerTypes = repLand.getDataValues("OwnerType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.LandTypes = repLand.getDataValues("LandType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            ViewBag.LandAreas = repLand.getDataValues("LandArea", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(Land);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Land_Edit(long id, ETLandDetails updateLand)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Convert.ToString(updateLand.AresSize)) && string.IsNullOrEmpty(Convert.ToString(updateLand.HectareSize)) && string.IsNullOrEmpty(Convert.ToString(updateLand.AcresSize)))
                {
                    ViewBag.messagealert = "Please Enter the Size of Land (Ares or Hectare or Acres).";
                    ViewBag.Districts = repLand.getDataValues("District", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Taluks = repLand.getDataValues("Taluk", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Divisions = repLand.getDataValues("Division", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Panchayats = repLand.getDataValues("Panchayat", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.Villages = repLand.getDataValues("Village", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.OwnerTypes = repLand.getDataValues("OwnerType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.LandTypes = repLand.getDataValues("LandType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    ViewBag.LandAreas = repLand.getDataValues("LandArea", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(updateLand);
                }
                else
                {
                    Land = new ETLandDetails();
                    Land = repLand.GetLand(id);

                    // Land Log
                    ETLandDetailsLog LandLog = new ETLandDetailsLog();
                    LandLog.LandID = Land.LandID;
                    LandLog.District = Land.District;
                    LandLog.Taluk = Land.Taluk;
                    LandLog.Division = Land.Division;
                    LandLog.Panchayat = Land.Panchayat;
                    LandLog.Village = Land.Village;
                    LandLog.OwnerType = Land.OwnerType;
                    LandLog.LandType = Land.LandType;
                    LandLog.LandArea = Land.LandArea;
                    LandLog.PattaNumber = Land.PattaNumber;
                    LandLog.PulaNumber = Land.PulaNumber;
                    LandLog.SubDivisionNumber = Land.SubDivisionNumber;
                    LandLog.OldSubDivisionNumber = Land.OldSubDivisionNumber;
                    LandLog.AcresSize = Land.AcresSize;
                    LandLog.AresSize = Land.AresSize;
                    LandLog.HectareSize = Land.HectareSize;
                    LandLog.OwnerName = Land.OwnerName;
                    LandLog.OwnerNameInTamil = Land.OwnerNameInTamil;
                    LandLog.Remarks = Land.Remarks;
                    LandLog.SourceType = "U";
                    LandLog.IsActive = Land.IsActive;
                    LandLog.IsOwned = Land.IsOwned;
                    LandLog.IsCommon = Land.IsCommon;
                    LandLog.IsVerified = Land.IsVerified;
                    LandLog.UserID = Land.UserID;
                    LandLog.CreatedDate = Land.CreatedDate;
                    LandLog.CreatedUser = Land.CreatedUser;
                    LandLog.ModifiedDate = DateTime.Now;
                    LandLog.ModifiedUser = Convert.ToInt64(Session["UserID"]);
                    dbEntities.ETLandDetailsLogs.Add(LandLog);

                    // Land Details
                    Land.District = updateLand.District;
                    Land.Taluk = updateLand.Taluk;
                    Land.Division = updateLand.Division;
                    Land.Panchayat = updateLand.Panchayat;
                    Land.Village = updateLand.Village;
                    Land.OwnerType = updateLand.OwnerType;
                    Land.LandType = updateLand.LandType;
                    Land.LandArea = updateLand.LandArea;
                    Land.PattaNumber = updateLand.PattaNumber;
                    Land.PulaNumber = updateLand.PulaNumber;
                    Land.SubDivisionNumber = updateLand.SubDivisionNumber;
                    Land.OldSubDivisionNumber = updateLand.OldSubDivisionNumber;
                    //Land.AcresSize = updateLand.AcresSize;
                    //Land.AresSize = updateLand.AresSize;
                    //Land.HectareSize = updateLand.HectareSize;
                    Land.OwnerName = updateLand.OwnerName;
                    Land.OwnerNameInTamil = updateLand.OwnerNameInTamil;
                    Land.Remarks = updateLand.Remarks;
                    Land.IsActive = updateLand.IsActive;
                    Land.IsOwned = updateLand.IsOwned;
                    Land.IsCommon = updateLand.IsCommon;
                    Land.IsVerified = updateLand.IsVerified;
                    Land.ModifiedUser = Convert.ToInt64(Session["UserID"]);
                    Land.ModifiedDate = DateTime.Now;


                    if (!string.IsNullOrEmpty(Convert.ToString(updateLand.AresSize)) && (string.IsNullOrEmpty(Convert.ToString(updateLand.HectareSize)) || string.IsNullOrEmpty(Convert.ToString(updateLand.AcresSize))))
                    {
                        Land.AresSize = updateLand.AresSize;
                        Land.HectareSize = updateLand.AresSize / 100;
                        Land.AcresSize = (Convert.ToDouble(updateLand.AresSize) * 0.0247105);
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(updateLand.HectareSize)) && (string.IsNullOrEmpty(Convert.ToString(updateLand.AresSize)) || string.IsNullOrEmpty(Convert.ToString(updateLand.AcresSize))))
                    {
                        Land.HectareSize = updateLand.HectareSize;
                        Land.AresSize = updateLand.HectareSize * 100;
                        Land.AcresSize = (Convert.ToDouble(updateLand.HectareSize) * 2.47105);
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(updateLand.AcresSize)) && (string.IsNullOrEmpty(Convert.ToString(updateLand.HectareSize)) || string.IsNullOrEmpty(Convert.ToString(updateLand.AresSize))))
                    {
                        Land.AcresSize = updateLand.AcresSize;
                        Land.AresSize = (Convert.ToDouble(updateLand.AcresSize) / 0.0247105);
                        Land.HectareSize = (Convert.ToDouble(updateLand.AcresSize) / 2.47105);
                    }
                    else
                    {
                        Land.AcresSize = updateLand.AcresSize;
                        Land.AresSize = updateLand.AresSize;
                        Land.HectareSize = updateLand.HectareSize;
                    }

                    dbEntities.Entry(Land).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Land.LandID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Land");
            }
            return View();
        }
        #endregion

        public ActionResult Land_View()
        {
            return View();
        }
    }
}