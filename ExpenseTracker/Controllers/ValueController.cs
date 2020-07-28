using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ValueController : BaseController
    {
        // GET: Value
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Value_add()
        {
            return View();
        }

        public ActionResult Value_edit()
        {
            return View();
        }

        public ActionResult Value_view()
        {
            return View();
        }
    }
}