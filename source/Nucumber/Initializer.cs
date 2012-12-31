using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Nucumber.Initializer), "RegisterFeatureRoutes")]
namespace Nucumber
{
    public class Initializer
    {
        public static void Initialise()
        {
            RegisterFeatureRoutes();
            InitialiseFeatureStore();
        }

        public static void RegisterFeatureRoutes()
        {
            RouteTable.Routes.MapRoute(
                "Feature", // Route name
                "Features/{action}/{id}", // URL with parameters
                new { controller = "Nucumber", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Nucumber.Controllers" } // namespace
            );
        }

        public static void InitialiseFeatureStore()
        {
            // TODO: Get this path dynamically
            FeatureStore.SetFeaturePath(@"C:\workspace\nucumber\source\Nucumber.Web\Nucumber.Web\Features");
        }
    }
}
