using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExpenseTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Sources",
            //    url: "{controller}",
            //    defaults: new { controller = "Sources", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Profiles",
            //    url: "{controller}/{id}",
            //    defaults: new { controller = "CommonUser", action = "Profiles", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Value", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
