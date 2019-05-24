using System.Web;
using System.Web.Optimization;

namespace SimpleCure
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/distribution/vendor/jquery/jquery.js",
                        "~/distribution/vendor/popper.js/umd/popper.min.js",
                        "~/distribution/vendor/bootstrap/js/bootstrap.min.js",
                        "~/distribution/js/grasp_mobile_progress_circle - 1.0.0.min.js",
                        "~/distribution/vendor/jquery.cookie/jquery.cookie.js",
                        "~/distribution/vendor/chart.js/Chart.min.js",
                        "~/distribution/vendor/jquery-validation/jquery.validate.min.js",
                        "~/distribution/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",                                      
                        "~/distribution/js/front.js",
                        "~/distribution/vendor/moment/moment.js",
                        "~/distribution/vendor/bootstrap-datetimepicker/bootstrap-4-datetimepicker.min.js"                        
                        )); 
 
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/distribution/vendor/bootstrap/css/bootstrap.min.css",
                      "~/distribution/vendor/font-awesome/css/font-awesome.min.css",
                      "~/distribution/css/fontastic.css",
                      "~/distribution/css/grasp_mobile_progress_circle-1.0.0.min.css",
                      "~/distribution/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css",
                      "~/distribution/css/style.green.css",
                      "~/distribution/css/custom.css",                      
                      "~/distribution/vendor/bootstrap-datetimepicker/bootstrap-4-datetimepicker.min.css"
                      ));

        }
    }
}
