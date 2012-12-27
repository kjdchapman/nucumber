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
               "FeatureNavigation", // Route name
               "Features", // URL with parameters
               new { controller = "FeatureNavigation", action = "Index"}, // Parameter defaults
               new[] { "Nucumber.Controllers" } // namespace
            );

            RouteTable.Routes.MapRoute(
                "FeatureDetail", // Route name
                "Features/{action}/{id}", // URL with parameters
                new { controller = "FeatureDetail", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Nucumber.Controllers" } // namespace
            );
        }
    }
}
