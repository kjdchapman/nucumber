using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Nucumber.Initializer), "RegisterFeatureRoutes")]
namespace Nucumber
{
    public class Initializer
    {
        public static void RegisterFeatureRoutes()
        {
            RouteTable.Routes.MapRoute(
                "Feature", // Route name
                "Features/{action}/{id}", // URL with parameters
                new { controller = "Nucumber", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Nucumber.Controllers" } // namespace
            );
        }
    }
}
