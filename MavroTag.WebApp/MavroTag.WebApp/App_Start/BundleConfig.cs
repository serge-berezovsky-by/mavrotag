using System.Web;
using System.Web.Optimization;

namespace MavroTag.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
#if !DEBUG
			BundleTable.EnableOptimizations = true;
#endif

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/Site.js"));
        }
    }
}
