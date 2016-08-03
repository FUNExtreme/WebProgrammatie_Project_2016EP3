using System.Web;
using System.Web.Optimization;

namespace YouthLocationBooking.Web
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

            bundles.Add(new StyleBundle("~/Assets/css/bundles/styles").Include(
                            "~/Assets/css/reset.css",
                            "~/Assets/css/forms.css",
                            "~/Assets/css/styles.css"
                        ));

            bundles.Add(new StyleBundle("~/Assets/css/bundles/mainstyles").Include(
                            "~/Assets/css/main/styles.css"
                        ));

            bundles.Add(new StyleBundle("~/Assets/css/bundles/panelstyles").Include(
                            "~/Assets/css/panel/styles.css"
                        ));
        }
    }
}
