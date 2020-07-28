using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class MenuController : BaseController
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETMenu Menu = new ETMenu();
        UserRepository repMenu = new UserRepository();
        List<ETMenu> Menus = new List<ETMenu>();
        // GET: Menu
        #region Menu List
        public ActionResult Index()
        {
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Menus = new List<ETMenu>();
            Menus = repMenu.GetAllMenu();
            return View(Menus);
        }

        #endregion

        #region Menu Add

        [HttpGet]
        public ActionResult Menu_add()
        {
            ViewBag.messagealert = string.Empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Menu_add(ETMenu Menu)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Menu != null)
                {
                    if (repMenu.MenuIsExist(Menu.MenuName, 0))
                    {
                        ViewBag.messagealert = "Menu already exist";
                        return View(Menu);
                    }
                    else
                    {
                        Menu.CreatedBy = Convert.ToInt64(Session["UserID"]);
                        Menu.CreatedDate = DateTime.Now;
                        Menu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                        Menu.ModifiedDate = DateTime.Now;
                        dbEntities.ETMenus.Add(Menu);
                        dbEntities.SaveChanges();
                        if (Menu.MenuID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        #endregion

        #region Menu Edit

        [HttpGet]
        public ActionResult Menu_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Menu = new ETMenu();
            Menu = repMenu.GetMenu(Id);
            return View(Menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Menu_edit(long id, ETMenu updateMenu)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                Menu = new ETMenu();
                Menu = repMenu.GetMenu(id);

                if (repMenu.MenuIsExist(updateMenu.MenuName, id))
                {
                    ViewBag.messagealert = "Menu already exist";
                    return View(Menu);
                }
                else
                {
                    Menu.MenuName = updateMenu.MenuName;
                    Menu.MenuUrl = updateMenu.MenuUrl;
                    Menu.OrderNo = updateMenu.OrderNo;
                    Menu.Status = updateMenu.Status;
                    Menu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Menu.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Menu).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Menu.MenuID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        #endregion

        #region Menu View
        public ActionResult Menu_view(long Id)
        {
            Menu = repMenu.GetMenu(Id);
            return View(Menu);
        }
        #endregion

        #region Menu Delete
        public bool MenuDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Menu = new ETMenu();
                Menu = dbEntities.ETMenus.Where(x => x.MenuID == id).SingleOrDefault();
                if (Menu != null)
                {
                    dbEntities.ETMenus.Remove(Menu);
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

        #region MenuUpdateStatus
        public bool MenuUpdateStatus(bool status, long Menuid)
        {
            Menu = new ETMenu();
            Menu = repMenu.GetMenu(Menuid);

            if (Menu != null)
            {
                if (status)
                {
                    Menu.Status = false;
                    Menu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Menu.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Menu.Status = true;
                    Menu.ModifiedBy = Convert.ToInt64(Session["UserID"]);
                    Menu.ModifiedDate = DateTime.Now;
                }
                dbEntities.Entry(Menu).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion
    }
}