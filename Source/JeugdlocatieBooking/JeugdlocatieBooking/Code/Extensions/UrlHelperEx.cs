namespace System.Web.Mvc
{
    public static class UrlHelperEx
    {
        public static string RouteAction(this UrlHelper helper, string actionName, string controllerName, string routeName)
        {
            return helper.RouteUrl(routeName, new { action = actionName, controller = controllerName });
        }
    }
}