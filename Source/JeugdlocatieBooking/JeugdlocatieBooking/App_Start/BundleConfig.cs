using System.Web;
using System.Web.Optimization;

namespace JeugdlocatieBooking
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/js/bootstrap.js"));
            */

            bundles.Add(new StyleBundle("~/Assets/css/bundles/bootstrap").Include(
                      "~/Assets/css/bootstrap.min.css"
                      ));
        }
    }
}
