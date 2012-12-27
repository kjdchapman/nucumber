using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Nucumber.Initializer), "RegisterFeatureHomePageRoute")]
// [assembly: WebActivator.PreApplicationStartMethod(typeof(Nucumber.Initializer), "RegisterNewControllerFactory")]
namespace Nucumber
{
    public class Initializer
    {
        public static void RegisterFeatureHomePageRoute()
        {            
            // If the client application tries to map this route, then it will fail and throw an Exception.
            RouteTable.Routes.MapRoute("FeatureHomePage", "features", new { controller = "Nucumber", action = "Index" }, new[] {"Nucumber.Controllers"});
        }
    }
}
