using System.Web.Mvc;

namespace YouthLocationBooking.Web.Areas.Panel
{
    public class PanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Panel_default",
                url: "Panel/{controller}/{action}/{id}",
                namespaces: new string[] {
                    "YouthLocationBooking.Web.Areas.Panel.Controllers"
                },
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}