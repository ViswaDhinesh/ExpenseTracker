using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class RoleController : BaseController
    {
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETRole Role = new ETRole();
        UserRepository repRole = new UserRepository();
        List<ETRole> Roles = new List<ETRole>();
        // GET: Role
        #region Role List
        public ActionResult Index()
        {
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Roles = new List<ETRole>();
            Roles = repRole.GetAllRole();
            return View(Roles);
        }

        #endregion

        #region Role Add

        [HttpGet]
        public ActionResult Role_add()
        {
            ViewBag.messagealert = string.Empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Role_add(ETRole Role)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Role != null)
                {
                    if (repRole.RoleIsExist(Role.RoleName, 0))
                    {
                        ViewBag.messagealert = "Role already exist";
                        return View(Role);
                    }
                    else
                    {
                        Role.CreatedBy = "Dinesh";//Session["UserName"].ToString();
                        Role.CreatedDate = DateTime.Now;
                        Role.ModifiedBy = "Pandiyan";//Session["UserName"].ToString();
                        Role.ModifiedDate = DateTime.Now;
                        dbEntities.ETRoles.Add(Role);
                        dbEntities.SaveChanges();
                        if (Role.RoleID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Role");
            }
            return View();
        }

        #endregion

        #region Role Edit

        [HttpGet]
        public ActionResult Role_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Role = new ETRole();
            Role = repRole.GetRole(Id);
            return View(Role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Role_edit(long id, ETRole updateRole)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                Role = new ETRole();
                Role = repRole.GetRole(id);

                if (repRole.RoleIsExist(updateRole.RoleName, id))
                {
                    ViewBag.messagealert = "Role already exist";
                    return View(Role);
                }
                else
                {
                    Role.RoleName = updateRole.RoleName;
                    Role.IsActive = updateRole.IsActive;
                    Role.ModifiedBy = "Dinesh"; //Session["UserName"].ToString();
                    Role.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Role).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Role.RoleID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Role");
            }
            return View();
        }

        #endregion

        #region Role View
        public ActionResult Role_view(long Id)
        {
            Role = repRole.GetRole(Id);
            return View(Role);
        }
        #endregion

        #region Role Delete
        public bool RoleDelete(long Roleid)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Role = new ETRole();
                Role = dbEntities.ETRoles.Where(x => x.RoleID == Roleid).SingleOrDefault();
                if (Role != null)
                {
                    dbEntities.ETRoles.Remove(Role);
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

        #region RoleUpdateStatus
        public bool RoleUpdateStatus(bool status, long Roleid)
        {
            Role = new ETRole();
            Role = repRole.GetRole(Roleid);

            if (Role != null)
            {
                if (status)
                {
                    Role.IsActive = false;
                    Role.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
                    Role.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Role.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
                    Role.ModifiedDate = DateTime.Now;
                    Role.IsActive = true;
                }
                dbEntities.Entry(Role).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion

        #region Menu_Mapping
        [HttpGet]
        public ActionResult Menu_Mapping(int id)
        {
            List<ETMenu> LstMenu = new List<ETMenu>();
            LstMenu = dbEntities.ETMenus.Where(n => n.Status == true).OrderBy(n => n.OrderNo).ToList();

            List<ETSubMenu> LstSubMenu = new List<ETSubMenu>();
            LstSubMenu = dbEntities.ETSubMenus.Where(n => n.Status == true).OrderBy(n => n.OrderNo).ToList();

            List<ETMenuAccess> LstRolemenumap = new List<ETMenuAccess>();
            ETMenuAccess objmenumapping = new ETMenuAccess();

            objmenumapping.lstrolemenumap = new List<ETMenuAccess>();
            foreach (var item in LstSubMenu)
            {
                LstRolemenumap = dbEntities.ETMenuAccesses.Where(n => n.SubMenuID == item.SubMenuID && n.RoleID == id).ToList();
                foreach (var roleitem in LstRolemenumap)
                {
                    ETMenuAccess objmenumappingnew = new ETMenuAccess();

                    objmenumappingnew.MenuID = roleitem.MenuID;
                    objmenumappingnew.SubMenuID = roleitem.SubMenuID;
                    objmenumappingnew.RoleID = id;
                    objmenumappingnew.Status = roleitem.Status;
                    objmenumapping.lstrolemenumap.Add(objmenumappingnew);
                }

            }

            objmenumapping.lstsubmenu = LstSubMenu;
            objmenumapping.lstmenu = LstMenu;
            return View(objmenumapping);
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Menu_Mapping(int id, ETMenuAccess objmenumap)
        {
            TempData["messagealert"] = string.Empty;
            dbEntities.ETMenuAccesses.Where(n => n.RoleID == id).ToList().ForEach(o => dbEntities.ETMenuAccesses.Remove(o));
            dbEntities.SaveChanges();
            foreach (var item in objmenumap.lstrolemenumap)
            {
                if (item.Status == true)
                {
                    ETMenuAccess objmenu = new ETMenuAccess();
                    objmenu.MenuID = item.MenuID;
                    objmenu.SubMenuID = item.SubMenuID;
                    objmenu.RoleID = id;
                    objmenu.Status = item.Status;
                    objmenu.CreatedBy = "Dinesh"; //Session["UserName"].ToString();
                    objmenu.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString());
                    objmenu.ModifiedBy = "Dinesh"; //Session["UserName"].ToString();
                    objmenu.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString());
                    dbEntities.ETMenuAccesses.Add(objmenu);
                    dbEntities.SaveChanges();
                }
            }
            TempData["messagealert"] = Status.Update;
            return RedirectToAction("/Index");
        }
        #endregion
    }
}