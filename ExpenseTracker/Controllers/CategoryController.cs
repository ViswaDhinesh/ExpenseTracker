using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        ExpenseTrackerEntites dbEntities = new ExpenseTrackerEntites();
        ETCategory Category = new ETCategory();
        SourceRepository repCategory = new SourceRepository();
        List<ETCategory> Categories = new List<ETCategory>();
        // GET: Category
        #region Category List
        public ActionResult Index()
        {
            ViewBag.messagealert = string.Empty;
            string messagealert = Convert.ToString(TempData["messagealert"]);
            if (!string.IsNullOrEmpty(messagealert))
            {
                ViewBag.messagealert = messagealert;
            }
            Categories = new List<ETCategory>();
            Categories = repCategory.GetAllCategory(1); // Need to change
            return View(Categories);
        }
        #endregion

        #region Category Add

        [HttpGet]
        public ActionResult Category_add()
        {
            ViewBag.messagealert = string.Empty;
            ViewBag.Source = repCategory.getSourceType();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category_add(ETCategory Category)
        {
            TempData["messagealert"] = string.Empty;
            ViewBag.messagealert = string.Empty;

            if (ModelState.IsValid)
            {
                if (Category != null)
                {
                    if (repCategory.CategoryIsExist(Category.CategoryName, 0))
                    {
                        ViewBag.messagealert = "Category already exist";
                        ViewBag.Source = repCategory.getSourceType();
                        return View(Category);
                    }
                    else
                    {
                        Category.UserID = 1; // Need to change
                        Category.CreatedBy = "Dinesh";//Session["UserName"].ToString();
                        Category.CreatedDate = DateTime.Now;
                        Category.ModifiedBy = "Pandiyan";//Session["UserName"].ToString();
                        Category.ModifiedDate = DateTime.Now;
                        dbEntities.ETCategories.Add(Category);
                        dbEntities.SaveChanges();
                        if (Category.CategoryID != 0)
                        {
                            TempData["messagealert"] = Status.Save;
                        }
                    }
                }
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        #endregion

        #region Category Edit

        [HttpGet]
        public ActionResult Category_edit(long Id)
        {
            ViewBag.messagealert = string.Empty;
            Category = new ETCategory();
            Category = repCategory.GetCategory(Id);
            ViewBag.Source = repCategory.getSourceType();
            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category_edit(long id, ETCategory updateCategory)
        {
            TempData["messagealert"] = string.Empty;

            if (ModelState.IsValid)
            {
                Category = new ETCategory();
                Category = repCategory.GetCategory(id);

                if (repCategory.CategoryIsExist(updateCategory.CategoryName, id))
                {
                    ViewBag.messagealert = "Category already exist";
                    ViewBag.Source = repCategory.getSourceType();
                    return View(Category);
                }
                else
                {
                    Category.CategoryName = updateCategory.CategoryName;
                    Category.CategoryTypeID = updateCategory.CategoryTypeID;
                    Category.IsActive = updateCategory.IsActive;
                    Category.ModifiedBy = "Dinesh"; //Session["UserName"].ToString();
                    Category.ModifiedDate = DateTime.Now;
                    dbEntities.Entry(Category).State = EntityState.Modified;
                    dbEntities.SaveChanges();
                    if (Category.CategoryID != 0)
                    {
                        TempData["messagealert"] = Status.Update;
                    }
                }
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        #endregion

        #region Category View
        public ActionResult Category_view(long Id)
        {
            Category = repCategory.GetCategory(Id);
            return View(Category);
        }
        #endregion

        #region Category Delete
        public bool CategoryDelete(long id)
        {
            if (!dbEntities.ETUsers.Where(x => x.UserID == 1).Any()) // Need to change
            {
                TempData["messagealert"] = Status.Delete;
                Category = new ETCategory();
                Category = dbEntities.ETCategories.Where(x => x.CategoryID == id).SingleOrDefault();
                if (Category != null)
                {
                    dbEntities.ETCategories.Remove(Category);
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

        #region CategoryUpdateStatus
        public bool CategoryUpdateStatus(bool status, long Categoryid)
        {
            Category = new ETCategory();
            Category = repCategory.GetCategory(Categoryid);

            if (Category != null)
            {
                if (status)
                {
                    Category.IsActive = false;
                    Category.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
                    Category.ModifiedDate = DateTime.Now;
                }
                else
                {
                    Category.IsActive = true;
                    Category.ModifiedBy = "Dinesh";//Session["UserName"].ToString();
                    Category.ModifiedDate = DateTime.Now;
                }
                dbEntities.Entry(Category).State = EntityState.Modified;
                dbEntities.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion
    }
}