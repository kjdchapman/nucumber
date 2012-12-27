using System.Web.Mvc;
using System.Web.Routing;

namespace Nucumber.Web
{
    public class RouteConfigurator
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Example", action = "Index", id = UrlParameter.Optional} // Parameter defaults
            );
        }
    }
}