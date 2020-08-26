using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class WindowsToWebController : Controller
    {
        // GET: WindowsToWeb
        public ActionResult Index()
        {
            ViewBag.messagealert = string.Empty;
            if (Request.QueryString["Message"] != null)
            {
                ViewBag.messagealert = Request.QueryString["Message"].ToString().Trim();
            }
            return View();
        }
    }
}