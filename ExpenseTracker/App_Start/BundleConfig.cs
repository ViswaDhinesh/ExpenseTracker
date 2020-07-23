using System.Web;
using System.Web.Optimization;

namespace ExpenseTracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/bundles/Admin/css").Include(
                "~/Content/Admin/css/bootstrap.css",
                "~/Content/Admin/css/font-awesome.min.css",
                "~/Content/Admin/css/bootstrap-datepicker3.min.css",
                "~/Content/Admin/css/style.css",
                "~/Content/Admin/css/Styles.css" //New
                ));

            bundles.Add(new StyleBundle("~/bundles/Front/css").Include(
             "~/Content/Front/css/jquery.toast.min.css",
             "~/Content/Front/css/bootstrap.min.css",
             "~/Content/Front/css/animate.css",
             "~/Content/Front/fonts/fonts.css",
             "~/Content/Front/css/owl.carousel.min.css",
             "~/Content/Front/css/hover.css",
             "~/Content/Front/css/flickity.min.css",
             "~/Content/Front/css/style.css",
             "~/Content/Front/css/responsive.css",
             "~/Content/Front/css/sweetalert2.min.css",
             "~/Content/Admin/css/fastselect.css",
             "~/Content/Front/css/jquery.mCustomScrollbar.css"
             ));

            bundles.Add(new ScriptBundle("~/bundles/Admin/js").Include(

                "~/Content/Admin/js/jquery-1.11.1.min.js",
                "~/Content/Admin/js/bootstrap.min.js",
                "~/Content/Admin/js/bootstrap-datepicker.min.js",
                "~/Content/Admin/js/datagridcustom.js",
                "~/Content/Admin/js/jquery.validate.js",
                "~/Content/Admin/js/jquery.validate.unobtrusive.js",
                "~/Content/Admin/js/common.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Front/js").Include(
               "~/Content/Front/js/modernizr.custom.js",
               "~/Content/Front/js/jquery.min.js",
               "~/Content/Front/js/popper.min.js",
               "~/Content/Front/js/jquery.toast.min.js",
               "~/Content/Front/js/bootstrap.min.js",
               "~/Content/Front/js/countUp.min.js",
               "~/Content/Front/js/wow.min.js",
               "~/Content/Front/js/owl.carousel.min.js",
               "~/Content/Front/js/flickity.pkgd.min.js",
               "~/Content/Front/js/jquery.mCustomScrollbar.min.js",
               //"~/Content/Front/js/bootnavbar.js",
               "~/Content/Front/js/sweetalert2.all.min.js",
               "~/Content/Front/js/jquery.nicescroll.min.js",
               "~/Content/Admin/js/fastselect.standalone.js",
               "~/Content/Front/js/main.js",
               "~/Content/Front/js/common.js"
               ));
            //}
            //{
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
