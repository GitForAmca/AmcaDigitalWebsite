using System.Web;
using System.Web.Optimization;

namespace AMCA_IT_SOLUTIONS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));


            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                                    "~/Scripts/bootstrap.bundle.min.js",
                                     "~/Scripts/aos.js",
                                "~/Scripts/owl.carousel.min.js",
                                "~/Scripts/dropdown-select.js",
                                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                                 //"~/Scripts/game-wraper.js",
                                 "~/Scripts/Site.js"));
            

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css",
                                                                 "~/Content/bootstrap.min.css",
                                                                    "~/Content/aos.css",
                                                                    "~/Content/owl.carousel.min.css",
                                                                    "~/Content/owl.theme.default.min.css",
                                                                    "~/Content/dropdown-select.css",
                                                                    "~/Content/Site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
