using System.Web.Mvc;
using System.Web.Routing;

namespace JeugdlocatieBooking
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}/{action}/",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "ActionOnly",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
