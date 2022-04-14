using System.Web;
using System.Web.Optimization;

namespace MvcApplication1
{
    public class BundleConfig
    {

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery.js",
                        "~/Content/js/bootstrap.js",
                        "~/Content/js/script3661.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/bootstrap3661.css",
                                                                 "~/Content/fonts/fontawesome/css/all.min.css",
                                                                 "~/Content/css/ui3661.css",
                                                                 "~/Content/css/responsive3661.css"));

            bundles.Add(new StyleBundle("~/Content/fonts").Include("~/Content/fonts/fontawesome/css/all_min.css"));

        }
    }
}