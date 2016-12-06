using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MehulIndustries.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/core").Include(
                "~/Content/plugins/jquery-circliful/css/jquery.circliful.css",
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/core.css",
                "~/Content/css/components.css",
                "~/Content/css/icons.css",
                "~/Content/css/pages.css",
                "~/Content/css/menu.css",
                "~/Content/css/responsive.css",
                "~/Content/css/AJAXLoader.css"
                ));
            bundles.Add(new StyleBundle("~/Content/css/datatablecss").Include(
               "~/Content/plugins/datatables/jquery.dataTables.min.css"
               ));
            bundles.Add(new StyleBundle("~/Content/css/customsweetcss").Include(
                "~/Content/plugins/custombox/dist/custombox.min.css",
                "~/Content/plugins/bootstrap-sweetalert/sweet-alert.css"
                ));
            bundles.Add(new StyleBundle("~/Content/css/customsweetswitcheryselect2css").Include(
                "~/Content/plugins/custombox/dist/custombox.min.css",
                "~/Content/plugins/bootstrap-sweetalert/sweet-alert.css",
                "~/Content/plugins/switchery/switchery.min.css",
                "~/Content/plugins/selectize/css/selectize.bootstrap3.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/js/datatablejs").Include(
              "~/Content/plugins/datatables/jquery.dataTables.min.js",
              "~/Content/plugins/datatables/dataTables.bootstrap.js"
              ));
            bundles.Add(new ScriptBundle("~/Content/css/dashboardjs").Include(
              "~/Content/plugins/waypoints/lib/jquery.waypoints.js",
              "~/Content/plugins/counterup/jquery.counterup.min.js",
              "~/Content/plugins/jquery-circliful/js/jquery.circliful.min.js",
              "~/Content/plugins/jquery-sparkline/jquery.sparkline.min.js",
              "~/Content/plugins/skyicons/skycons.min.js",
              "~/Content/pages/jquery.dashboard.js"
              ));
            bundles.Add(new ScriptBundle("~/Content/css/customsweetjs").Include(
               "~/Content/plugins/custombox/dist/custombox.min.js",
               "~/Content/plugins/bootstrap-sweetalert/sweet-alert.min.js"
               ));
            bundles.Add(new ScriptBundle("~/Content/css/customsweetswitcheryselect2js").Include(
              "~/Content/plugins/custombox/dist/custombox.min.js",
              "~/Content/plugins/bootstrap-sweetalert/sweet-alert.min.js",
              "~/Content/plugins/switchery/switchery.min.js",
              "~/Content/plugins/selectize/js/standalone/selectize.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
               "~/Content/js/html5shiv.js",
                "~/Content/js/respond.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            "~/Content/js/modernizr.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Content/js/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryvalidate").Include(
                         "~/Content/js/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/corejs").Include(
                         "~/Content/js/bootstrap.min.js",
                         "~/Content/js/detect.js",
                         "~/Content/js/fastclick.js",
                         "~/Content/js/jquery.blockUI.js",
                         "~/Content/js/waves.js",
                         "~/Content/js/wow.min.js",
                         "~/Content/js/jquery.nicescroll.js",
                         "~/Content/js/jquery.scrollTo.min.js",
                         "~/Content/plugins/notifyjs/dist/notify.min.js",
                         "~/Content/plugins/notifications/notify-metro.js",
                         "~/Content/js/jquery.core.js",
                         "~/Content/js/jquery.app.js",
                         "~/Content/js/site.js"));
            BundleTable.EnableOptimizations = true;
        }
    }
}