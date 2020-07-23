using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class SourcesController : BaseController
    {

        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETSource Sourcee = new ETSource();
        SourceRepository repSource = new SourceRepository();
        List<ETSource> Sources = new List<ETSource>();
        // GET: Source
        #region Source List
        public ActionResult Index()
        {
            ViewBag.UserPermission = Session["UserLevel"].ToString().ToUpper();
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Sources = new List<ETSource>();
            Sources = repSource.GetAllSource(Convert.ToInt64(Session["UserID"]), Session["UserLevel"].ToString());
            return View(Sources);
        }

        #endregion

        #region Source Add

        [HttpGet]
        public ActionResult Source_add()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.SourceTypes = repSource.getDataValues("SourceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Source_add(ETSource Source)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Source != null)
                {
                    if (repSource.SourceIsExist(Source.SourceName, 0))
                    {
                        ViewBag.messagealert = "Source already exist";
                        ViewBag.SourceTypes = repSource.getDataValues("SourceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                        return View(Source);
                    }
                    else
                    {
                        Source.UserID = Convert.ToInt64(Session["UserID"]);
                        Source.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        Source.CreatedDate = DateTime.Now;
                        Source.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        Source.ModifiedDate = DateTime.Now;
                        dbEntities.ETSources.Add(Source);
                        dbEntities.SaveChanges();
                        if (Source.SourceID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Sources");
            }
            return View();
        }

        #endregion

        #region Source Edit

        [HttpGet]
        public ActionResult Source_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Sourcee = new ETSource();
            Sourcee = repSource.GetSource(Id);
            ViewBag.SourceTypes = repSource.getDataValues("SourceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
            return View(Sourcee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Source_edit(long id, ETSource updateSource)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                Sourcee = new ETSource();
                Sourcee = repSource.GetSource(id);

                if (repSource.SourceIsExist(updateSource.SourceName, id))
                {
                    ViewBag.messagealert = "Source already exist";
                    ViewBag.SourceTypes = repSource.getDataValues("SourceType", Session["UserLevel"].ToString(), Convert.ToInt64(Session["UserID"]), Convert.ToInt64(Session["ReportingUser"]));
                    return View(Sourcee);
                }
                else
                {
                    Sourcee.SourceName = updateSource.SourceName;
                    Sourcee.SourceType = updateSource.SourceType;
                    Sourcee.IsActive = updateSource.IsActive;
                    Sourcee.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Sourcee.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Sourcee).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Sourcee.SourceID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Sources");
            }
            return View();
        }

        #endregion

        #region Source View
        public ActionResult Source_view(long Id)
        {
            Sourcee = repSource.GetSource(Id);
            return View(Sourcee);
        }
        #endregion

        #region Source Delete
        public bool SourceDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Sourcee = new ETSource();
                Sourcee = dbEntities.ETSources.Where(x => x.SourceID == id).SingleOrDefault();
                if (Sourcee != null)
                {
                    dbEntities.ETSources.Remove(Sourcee);
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

        #region SourceUpdateStatus
        public bool SourceUpdateStatus(bool status, long Sourceid)
        {
            Sourcee = new ETSource();
            Sourcee = repSource.GetSource(Sourceid);

            if (Sourcee != null)
            {
                if (status)
                {
                    Sourcee.IsActive = false;
                    Sourcee.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Sourcee.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Sourcee.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Sourcee.ModifiedDate = DateTime.Now;
                    Sourcee.IsActive = true;
                }
                dbEntities.Entry(Sourcee).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion

        public ActionResult Source()
        {
            return View();
        }
    }
}