using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FL_ACME.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            //css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/dist/css/adminlte.css",
                "~/plugins/fontawesome-free/css/all.css",
                "~/plugins/icheck-bootstrap/icheck-bootstrap.css"
                ));

            //lib css
            bundles.Add(new StyleBundle("~/Content/libcss").Include(
                "~/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.css",
                "~/plugins/select2/css/select2.css",
                //"~/plugins/select2-bootstrap4-theme/select2-bootstrap4.css",
                "~/plugins/jqvmap/jqvmap.css",
                "~/plugins/overlayScrollbars/css/OverlayScrollbars.css",
                //"~/plugins/daterangepicker/daterangepicker.css",
                //"~/plugins/summernote/summernote-bs4.css",
                "~/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.css",
                "~/plugins/datatables-bs4/css/dataTables.bootstrap4.css",
                "~/plugins/raty/jquery.raty.css",
                "~/dist/js/googlefonts.css",
                "~/plugins/toastr/toastr.css",
                "~/plugins/UploaderImage/image-uploader.css"
                ));

            //lib css
            //bundles.Add(new StyleBundle("~/Content/toastr").Include(
            //    "~/plugins/toastr/toastr.css"));


            // JQuery

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/plugins/jquery/jquery.js",
                      "~/plugins/bootstrap/js/bootstrap.js",
                      "~/plugins/bootstrap/js/bootstrap.bundle.js"));



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/plugins/jquery-ui/jquery-ui.js",
                "~/dist/js/jquery.validate.js",
                "~/scripts/jquery.unobtrusive-ajax.js",
                "~/dist/js/jquery.validate.unobtrusive.js"
                     ));

            //JQ lib
            bundles.Add(new ScriptBundle("~/bundles/libjs").Include(
              "~/plugins/jqvmap/jquery.vmap.js",
              "~/plugins/moment/moment.js",
              "~/plugins/daterangepicker/daterangepicker.js",
              "~/dist/js/adminlte.js",
              "~/dist/js/pages/dashboard.js",
              "~/dist/js/demo.js",
              "~/plugins/toastr/toastr.js",
              "~/plugins/select2/js/select2.js",
              "~/plugins/overlayScrollbars/js/jquery.overlayScrollbars.js",
              "~/plugins/bs-custom-file-input/bs-custom-file-input.js",
              "~/dist/js/adminlte.js"
              , "~/plugins/raty/jquery.raty.js",
              "~/plugins/sweetalert2/sweetalert2.js",
              "~/plugins/toastr/toastr.js",
              "~/plugins/moment/moment.js",
               "~/plugins/daterangepicker/daterangepicker.js",
                "~/plugins/UploaderImage/image-uploader.js"

              ));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
              "~/plugins/datatables/jquery.dataTables.js",
              "~/plugins/datatables-bs4/js/dataTables.bootstrap4.js"
              ));

            //bundles.Add(new ScriptBundle("~/bundles/chart").Include(
            //   "~/plugins/chart.js/Chart.js"));
        }
    }
}