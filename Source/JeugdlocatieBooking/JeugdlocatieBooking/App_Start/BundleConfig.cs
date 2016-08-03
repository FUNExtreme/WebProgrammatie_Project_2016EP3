using System.Web;
using System.Web.Optimization;
using YouthLocationBooking.Web.Code;

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

            var bundle = new StyleBundle("~/Assets/css/bundles/styles");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle.Include("~/Assets/css/bootstrap.min.css",
                            "~/Assets/css/font-awesome.min.css",
                            "~/Assets/css/reset.css",
                            "~/Assets/css/forms.css",
                            "~/Assets/css/styles.css");
            bundles.Add(bundle);

            bundles.Add(new StyleBundle("~/Assets/css/bundles/mainstyles").Include(
                            "~/Assets/css/main/styles.css"
                        ));

            bundles.Add(new StyleBundle("~/Assets/css/bundles/panelstyles").Include(
                            "~/Assets/css/panel/styles.css"
                        ));
        }
    }
}
