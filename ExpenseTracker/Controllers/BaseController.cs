using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    //[ExceptionHandler]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["RoleID"] != null)
            {
                long roleId = Convert.ToInt64(Session["RoleID"]);
                string Url = Request.Url.AbsolutePath;
                string[] UrlSplit = Url.Split('/');
                Url = "/" + UrlSplit[1] + "/";

                ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();

                long subMenuId = 0;
                subMenuId = dbEntities.ETSubMenus.Where(n => n.SubMenuUrl.ToLower().Contains(Url.ToLower()) && n.Status).FirstOrDefault().SubMenuID;


                bool isPermission = dbEntities.ETMenuAccesses.Where(x => x.RoleID == roleId).Any(x => x.SubMenuID == subMenuId);
                if (!isPermission)
                {
                    Session["UserID"] = null;
                    Session["UserName"] = null;
                    Session["RoleID"] = null;
                    Session["RoleName"] = null;
                    Session["UserLevel"] = null;
                    Session["LoginName"] = null;
                    Session["Email"] = null;
                    Session["Phone"] = null;
                    Session["LastName"] = null;
                    Session["IsTwoFactor"] = null;
                    Session["UserLevel"] = null;
                    Session["ReportingUser"] = null;
                    Session["MappedUser"] = null;
                    TempData["SessionExpired"] = "You don't have permission to access this page";
                    Response.Redirect("/Login");
                }
                else
                {
                    List<ETMenuAccess> menuMappings = new List<ETMenuAccess>();
                    List<ETMenu> menus = new List<ETMenu>();
                    List<ETSubMenu> subMenus = new List<ETSubMenu>();
                    List<ETSubMenu> currentSubMenus = new List<ETSubMenu>();
                    List<long> menuIds = new List<long>();
                    List<long> subMenuIds = new List<long>();

                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])) && !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])))
                    {
                        long userId = Convert.ToInt64(Session["UserID"]);
                        int count = dbEntities.ETUsers.Where(x => x.UserID == userId && x.IsActive).Count();
                        if (count == 0)
                        {
                            Session["UserID"] = null;
                            Session["UserName"] = null;
                            Session["RoleID"] = null;
                            Session["RoleName"] = null;
                            Session["UserLevel"] = null;
                            Session["LoginName"] = null;
                            Session["Email"] = null;
                            Session["Phone"] = null;
                            Session["LastName"] = null;
                            Session["IsTwoFactor"] = null;
                            Session["UserLevel"] = null;
                            Session["ReportingUser"] = null;
                            Session["MappedUser"] = null;
                            TempData["SessionExpired"] = "Sorry, Your account was deactivated";
                            Response.Redirect("/Login");
                        }
                        else
                        {
                            menuMappings = dbEntities.ETMenuAccesses.Where(x => x.RoleID == roleId).ToList();
                            if (menuMappings != null && menuMappings.Count > 0)
                            {
                                menuIds = menuMappings.Select(x => x.MenuID).Distinct().ToList();
                                subMenuIds = menuMappings.Select(x => x.SubMenuID).Distinct().ToList();
                                menus = dbEntities.ETMenus.Where(x => menuIds.Contains(x.MenuID) && x.Status).OrderBy(x => x.OrderNo).ThenBy(x => x.MenuName).ToList();
                                subMenus = dbEntities.ETSubMenus.Where(x => subMenuIds.Contains(x.SubMenuID) && x.Status).OrderBy(x => x.OrderNo).ThenBy(x => x.SubMenuName).ToList();

                                ViewBag.menus = menus;
                                ViewBag.subMenus = subMenus;
                            }
                            else
                            {
                                Session["UserID"] = null;
                                Session["UserName"] = null;
                                Session["RoleID"] = null;
                                Session["RoleName"] = null;
                                Session["UserLevel"] = null;
                                Session["LoginName"] = null;
                                Session["Email"] = null;
                                Session["Phone"] = null;
                                Session["LastName"] = null;
                                Session["IsTwoFactor"] = null;
                                Session["UserLevel"] = null;
                                Session["ReportingUser"] = null;
                                Session["MappedUser"] = null;
                                TempData["SessionExpired"] = "Sorry, You don't have permission";
                                Response.Redirect("/Login");
                            }
                        }
                    }
                    else
                    {
                        Session["UserID"] = null;
                        Session["UserName"] = null;
                        Session["RoleID"] = null;
                        Session["RoleName"] = null;
                        Session["UserLevel"] = null;
                        Session["LoginName"] = null;
                        Session["Email"] = null;
                        Session["Phone"] = null;
                        Session["LastName"] = null;
                        Session["IsTwoFactor"] = null;
                        Session["UserLevel"] = null;
                        Session["ReportingUser"] = null;
                        Session["MappedUser"] = null;
                        TempData["SessionExpired"] = "Session Expired";
                        Response.Redirect("/Login");
                    }
                }
            }
            else
            {
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["RoleID"] = null;
                Session["RoleName"] = null;
                Session["UserLevel"] = null;
                Session["LoginName"] = null;
                Session["Email"] = null;
                Session["Phone"] = null;
                Session["LastName"] = null;
                Session["IsTwoFactor"] = null;
                Session["UserLevel"] = null;
                Session["ReportingUser"] = null;
                Session["MappedUser"] = null;
                Response.Redirect("/Login");
            }

        }
    }
}