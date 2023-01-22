using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WebPortal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper_js").Include(
                "~/Scripts/popper.min.js"));

            //bootstrap bundles
            bundles.Add(new Bundle("~/bundles/bootstrap_js").Include(
                "~/Scripts/jquery-3.6.0.min.js",
                "~/Scripts/bootstrap.bundle.min.js"));

            //fancybox
            bundles.Add(new ScriptBundle("~/bundles/fancybox_js").Include(
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/jquery.fancybox.pack.js",
                "~/Scripts/jquery.fancybox-buttons.js",
                "~/Scripts/jquery.fancybox-thumbs.js",
                "~/Scripts/jquery.fancybox-media.js"));

            //jquery ui
            bundles.Add(new ScriptBundle("~/bundles/jquery_ui_js").Include(
                "~/Scripts/jquery-ui-1.13.2.js",
                "~/Scripts/jquery-ui-touch-punch.js",
                "~/Scripts/jquery.mousewheel-3.0.6.pack.js",
                "~/Scripts/jquery-ui-sliderAccess.js"));

            //jquery validation
            bundles.Add(new ScriptBundle("~/bundles/jquery_validate_js").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/additional-methods.min.js"));

            //datatable
            bundles.Add(new ScriptBundle("~/bundles/datatable_js").Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap4.min.js",
                "~/Scripts/dataTable_moment.js"
                //"~/Scripts/DataTables/responsive.bootstrap4.min.js",
                //"~/Scripts/DataTables/dataTables.responsive.min.js"
                ));


            //moment
            bundles.Add(new ScriptBundle("~/bundles/moment_js").Include(
                "~/Scripts/moment.min.js"));

            //date/time picker
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker_js").Include(
                "~/Scripts/jquery-ui-timepicker-addon.min.js"));

            //bootstrap select
            bundles.Add(new ScriptBundle("~/bundles/bootstrap_select_js").Include(
                "~/Scripts/bootstrap-select.min.js"));

            //jquery confirm
            bundles.Add(new ScriptBundle("~/bundles/jquery_confirm_js").Include(
                "~/Scripts/jquery-confirm.min.js"));
            //boostrap multiselect
            bundles.Add(new ScriptBundle("~/bundles/boostrap_multiselect_js").Include(
                "~/Scripts/bootstrap-multiselect.min.js"));
            //input mask
            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            "~/Scripts/inputmask/jquery.inputmask.js"));

            //underscore js
            bundles.Add(new ScriptBundle("~/bundles/underscore_js").Include(
                "~/Scripts/underscore.min.js"));

            //bootstrap css
            bundles.Add(new StyleBundle("~/bundles/bootstrap_css").Include(
                "~/Content/bootstrap.min.css"));

            //jquery ui
            bundles.Add(new StyleBundle("~/bundles/jquery_ui_css").Include(
                "~/Content/themes/base/jquery-ui.css"
                ));
            //
            bundles.Add(new StyleBundle("~/bundles/jquery_ui_theme").Include(
              "~/Content/themes/base/jquery.ui.core.css",
               "~/Content/themes/base/jquery.ui.resizable.css",
               "~/Content/themes/base/jquery.ui.selectable.css",
               "~/Content/themes/base/jquery.ui.accordion.css",
               "~/Content/themes/base/jquery.ui.autocomplete.css",
               "~/Content/themes/base/jquery.ui.button.css",
               "~/Content/themes/base/jquery.ui.dialog.css",
               "~/Content/themes/base/jquery.ui.slider.css",
               "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
               "~/Content/themes/base/jquery.ui.progressbar.css",
               "~/Content/themes/base/jquery.ui.theme.css"));
            //fancybox
            bundles.Add(new StyleBundle("~/bundles/fancybox_css").Include(
                "~/Content/jquery.fancybox.css",
                "~/Content/jquery.fancybox-buttons.css",
                "~/Content/jquery.fancybox-thumbs.css"
                ));

            //datatable
            bundles.Add(new StyleBundle("~/bundles/datatable_css").Include(
                //"~/Content/DataTables/css/jquery.dataTables.min.js",
                "~/Content/DataTables/css/dataTables.bootstrap4.min.css"
                //"~/Content/DataTables/css/responsive.bootstrap4.min.css"
                ));

            //date/time pcker
            bundles.Add(new StyleBundle("~/bundles/datetimepicker_css").Include(
                "~/Content/jquery-ui-timepicker-addon.min.css"));

            //site css
            bundles.Add(new StyleBundle("~/bundles/site_css").Include(
                "~/Content/Site.css"));

            //default css
            bundles.Add(new StyleBundle("~/bundles/default_css").Include(
                //"~/Content/default.css",
                "~/Content/style_admin.css"
            ));

                // loader css
            bundles.Add(new StyleBundle("~/bundles/loader_css").Include(
                //"~/Content/default.css",
                "~/Content/loader.css"
            ));

            //styles
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/style.css"));

            //fontawesome
            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include(
                "~/Content/font-awesome.min.css"));

            //bootstrap select
            bundles.Add(new StyleBundle("~/bundles/bootstrap_select_css").Include(
                "~/Content/bootstrap-select.min.css"));

            //jquery confirm
            bundles.Add(new StyleBundle("~/bundles/jquery_confirm_css").Include(
                "~/Content/jquery-confirm.min.css"));

            //icheck bootstrap
            bundles.Add(new StyleBundle("~/bundles/icheck_bootstrap").Include(
                "~/Content/icheck-bootstrap.min.css"));
            //boostrap multiselect 
            bundles.Add(new StyleBundle("~/bundles/boostrap_multiselect_css").Include(
                "~/Content/bootstrap-multiselect.min.css"));

            //tinymce
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.min.js",
                "~/Scripts/tinymce/themes/silver/theme.min.js"));

            //popup
            bundles.Add(new ScriptBundle("~/bundles/popup").Include(
                "~/Scripts/popup.js"));

            //dt error handler
            bundles.Add(new ScriptBundle("~/bundles/dt_error_handler").Include(
                "~/Scripts/datatable_error_handler.js"));

            //main js
            bundles.Add(new ScriptBundle("~/bundles/main_js").Include(
                "~/Scripts/main.js"));
        }
    }
}