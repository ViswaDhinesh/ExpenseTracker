using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class SubMenuController : BaseController
    {
        // GET: SubMenu
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETSubMenu SubMenu = new ETSubMenu();
        UserRepository repSubMenu = new UserRepository();
        List<ETSubMenu> SubMenus = new List<ETSubMenu>();
        // GET: SubMenu
        #region SubMenu List
        public ActionResult Index()
        {
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            SubMenus = new List<ETSubMenu>();
            SubMenus = repSubMenu.GetAllSubMenu();
            return View(SubMenus);
        }
        #endregion

        #region SubMenu Add

        [HttpGet]
        public ActionResult SubMenu_add()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.MenuList = repSubMenu.getMenuType();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubMenu_add(ETSubMenu SubMenu)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (SubMenu != null)
                {
                    if (repSubMenu.SubMenuIsExist(SubMenu.SubMenuName, 0))
                    {
                        ViewBag.messagealert = "SubMenu already exist";
                        ViewBag.MenuList = repSubMenu.getMenuType();
                        return View(SubMenu);
                    }
                    else
                    {
                        SubMenu.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        SubMenu.CreatedDate = DateTime.Now;
                        SubMenu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        SubMenu.ModifiedDate = DateTime.Now;
                        dbEntities.ETSubMenus.Add(SubMenu);
                        dbEntities.SaveChanges();
                        if (SubMenu.SubMenuID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "SubMenu");
            }
            return View();
        }

        #endregion

        #region SubMenu Edit

        [HttpGet]
        public ActionResult SubMenu_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            SubMenu = new ETSubMenu();
            SubMenu = repSubMenu.GetSubMenu(Id);
            ViewBag.MenuList = repSubMenu.getMenuType();
            return View(SubMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubMenu_edit(long id, ETSubMenu updateSubMenu)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                SubMenu = new ETSubMenu();
                SubMenu = repSubMenu.GetSubMenu(id);

                if (repSubMenu.SubMenuIsExist(updateSubMenu.SubMenuName, id))
                {
                    ViewBag.messagealert = "SubMenu already exist";
                    ViewBag.MenuList = repSubMenu.getMenuType();
                    return View(SubMenu);
                }
                else
                {
                    try
                    {
                        //SubMenu.MenuName = string.Empty;
                        SubMenu.MenuID = updateSubMenu.MenuID;
                        SubMenu.SubMenuName = updateSubMenu.SubMenuName;
                        SubMenu.SubMenuUrl = updateSubMenu.SubMenuUrl;
                        SubMenu.OrderNo = updateSubMenu.OrderNo;
                        SubMenu.Status = updateSubMenu.Status;
                        SubMenu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        SubMenu.ModifiedDate = DateTime.Now;
                        dbEntities.Entry(SubMenu).State = EntityState.Modified;
                        dbEntities.SaveChanges();
                        if (SubMenu.SubMenuID != 0)
                        {
                            TempData["messagealert"] = Status.Update;
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }
                return RedirectToAction("Index", "SubMenu");
            }
            return View();
        }

        #endregion

        #region SubMenu View
        public ActionResult SubMenu_view(long Id)
        {
            SubMenu = repSubMenu.GetSubMenu(Id);
            return View(SubMenu);
        }
        #endregion

        #region SubMenu Delete
        public bool SubMenuDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                SubMenu = new ETSubMenu();
                SubMenu = dbEntities.ETSubMenus.Where(x => x.SubMenuID == id).SingleOrDefault();
                if (SubMenu != null)
                {
                    dbEntities.ETSubMenus.Remove(SubMenu);
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

        #region SubMenuUpdateStatus
        public bool SubMenuUpdateStatus(bool status, long SubMenuid)
        {
            SubMenu = new ETSubMenu();
            SubMenu = repSubMenu.GetSubMenu(SubMenuid);

            if (SubMenu != null)
            {
                if (status)
                {
                    SubMenu.Status = false;
                    SubMenu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    SubMenu.ModifiedDate = DateTime.Now;
                }
                else
                {
                    SubMenu.Status = true;
                    SubMenu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    SubMenu.ModifiedDate = DateTime.Now;
                }
                dbEntities.Entry(SubMenu).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion
    }
}