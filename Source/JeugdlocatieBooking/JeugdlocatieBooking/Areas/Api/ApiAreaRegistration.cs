using System.Web.Mvc;

namespace YouthLocationBooking.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Api_default",
                url: "Api/{controller}/{action}/{id}",
                namespaces: new string[] {
                    "YouthLocationBooking.Areas.Api.Controllers"
                },
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}