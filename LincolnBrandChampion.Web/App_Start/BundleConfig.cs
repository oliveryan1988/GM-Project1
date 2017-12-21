using System.Web;
using System.Web.Optimization;

namespace LincolnBrandChampion
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery.validate.js",
                         "~/Scripts/jquery.validate.unobtrusive.js"
                       ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/stylesheets").Include(
                        "~/stylesheets/foundation.css",
                          "~/stylesheets/rateit.css",
                        "~/stylesheets/jquery-ui.css",
                        "~/stylesheets/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/javascripts/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                        "~/javascripts/foundation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                       "~/javascripts/app.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}