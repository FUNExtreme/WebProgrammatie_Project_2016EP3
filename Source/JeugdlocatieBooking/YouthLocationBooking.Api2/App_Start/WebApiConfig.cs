using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace YouthLocationBooking.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if DEBUG
            // Always return JSON in debug builds, so the responses in browser are the same as the requested responses using application/json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
