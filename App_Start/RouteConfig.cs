using System.Web.Mvc;
using System.Web.Routing;

namespace flair.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("regular/{badge}.png");

            routes.MapRoute(
                name: "Badge",
                url: "Badge/{userId}",
                defaults: new { controller = "Badge", action = "Index", userId = 0 }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}