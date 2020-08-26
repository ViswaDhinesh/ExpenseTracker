using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class BankController : BaseController
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETBank Bank = new ETBank();
        SourceRepository repBank = new SourceRepository();
        List<ETBank> Banks = new List<ETBank>();
        // GET: Bank

        #region Bank List
        public ActionResult Index()
        {
            ViewBag.UserPermission = Session["UserLevel"].ToString().ToUpper();
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Banks = new List<ETBank>();
            Banks = repBank.GetAllBank(Convert.ToInt64(Session["UserID"]), Session["UserLevel"].ToString());
            return View(Banks);
        }
        #endregion

        #region Bank Add

        [HttpGet]
        public ActionResult Bank_add()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bank_add(ETBank Bank)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Bank != null)
                {
                    if (repBank.AccountNumberIsExist(Bank.AccountNumber, 0))
                    {
                        ViewBag.messagealert = "Account Number already exist";
                        ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        return View(Bank);
                    }
                    else if (Bank.CustomerID != null && Bank.CustomerID != "" && repBank.CustomerIDIsExist(Bank.CustomerID, 0))
                    {
                        ViewBag.messagealert = "Customer ID already exist";
                        ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        return View(Bank);
                    }
                    else if (repBank.BankEmailIsExist(Bank.Email, Convert.ToInt64(Session["UserID"])) || !Common.IsValidEmail(Bank.Email))
                    {
                        if (!Common.IsValidEmail(Bank.Email))
                            ViewBag.messagealert = "Please Enter Valid Email";
                        else
                            ViewBag.messagealert = "Email already exist";
                        ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        return View(Bank);
                    }
                    else
                    {
                        Bank.UserID = Convert.ToInt64(Session["UserID"]);
                        Bank.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        Bank.CreatedDate = DateTime.Now;
                        Bank.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        Bank.ModifiedDate = DateTime.Now;
                        dbEntities.ETBanks.Add(Bank);
                        dbEntities.SaveChanges();
                        if (Bank.BankID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Bank");
            }
            return View();
        }

        #endregion

        #region Bank Edit

        [HttpGet]
        public ActionResult Bank_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Bank = new ETBank();
            Bank = repBank.GetBank(Id);
            ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(Bank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bank_edit(long id, ETBank updateBank)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                Bank = new ETBank();
                Bank = repBank.GetBank(id);

                if (repBank.AccountNumberIsExist(updateBank.AccountNumber, id))
                {
                    ViewBag.messagealert = "Account Number already exist";
                    ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(Bank);
                }
                else if (updateBank.CustomerID != null && updateBank.CustomerID != "" && repBank.CustomerIDIsExist(updateBank.CustomerID, id))
                {
                    ViewBag.messagealert = "Customer ID already exist";
                    ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(Bank);
                }
                else if (repBank.BankEmailIsExist(updateBank.Email, Convert.ToInt64(Session["UserID"])) || !Common.IsValidEmail(updateBank.Email))
                {
                    if (!Common.IsValidEmail(updateBank.Email))
                        ViewBag.messagealert = "Please Enter Valid Email";
                    else
                        ViewBag.messagealert = "Email already exist";
                    ViewBag.AccountTypes = repBank.getDataValues("AccountType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(Bank);
                }
                else
                {
                    Bank.AccountHolder = updateBank.AccountHolder;
                    Bank.AccountNumber = updateBank.AccountNumber;
                    Bank.IFSCCode = updateBank.IFSCCode;
                    Bank.BankName = updateBank.BankName;
                    Bank.BranchName = updateBank.BranchName;
                    Bank.AccountType = updateBank.AccountType;
                    Bank.MinimumBalance = updateBank.MinimumBalance;
                    Bank.AccountOpeningDate = updateBank.AccountOpeningDate;
                    Bank.CustomerID = updateBank.CustomerID;
                    Bank.Password = updateBank.Password;
                    Bank.Email = updateBank.Email;
                    Bank.Phone = updateBank.Phone;
                    Bank.ATMNumber = updateBank.ATMNumber;
                    Bank.PINNumber = updateBank.PINNumber;
                    Bank.ExpiredDate = updateBank.ExpiredDate;
                    Bank.CVV = updateBank.CVV;
                    Bank.IsCreditCard = updateBank.IsCreditCard;
                    Bank.IsActive = updateBank.IsActive;
                    Bank.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Bank.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Bank).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Bank.BankID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Bank");
            }
            return View();
        }

        #endregion

        #region Bank View
        public ActionResult Bank_view(long Id)
        {
            Bank = repBank.GetBank(Id);
            return View(Bank);
        }
        #endregion

        #region Bank Delete
        public bool BankDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Bank = new ETBank();
                Bank = dbEntities.ETBanks.Where(x => x.BankID == id).SingleOrDefault();
                if (Bank != null)
                {
                    dbEntities.ETBanks.Remove(Bank);
                    dbEntities.SaveChanges();
                    return true;
                }
            }
            return false;

            //if (!dbEntities.TBL_ADMIN_USER.Where(x => x.ROLE_ID == id).Any())
            //{
            //    TempData["messagealert"] = Status.Delete;
            //    role = new TBL_ROLE();
            //    role = dbEntities.TBL_ROLE.Where(x => x.ROLE_ID == id && x.ROLE_NAME != "superadmin").SingleOrDefault();
            //    if (role != null)
            //    {
            //        dbEntities.TBL_ROLE.Remove(role);
            //        dbEntities.SaveChanges();
            //        return true;
            //    }
            //}
            //return false;
        }

        #endregion

        #region BankUpdateStatus
        public bool BankUpdateStatus(bool status, long Bankid)
        {
            Bank = new ETBank();
            Bank = repBank.GetBank(Bankid);

            if (Bank != null)
            {
                if (status)
                {
                    Bank.IsActive = false;
                    Bank.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Bank.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Bank.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Bank.ModifiedDate = DateTime.Now;
                    Bank.IsActive = true;
                }
                dbEntities.Entry(Bank).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion
    }
}