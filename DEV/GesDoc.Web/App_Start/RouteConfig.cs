using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace GesDoc.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            // mudar de parent para off para poder postar via ajax
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
