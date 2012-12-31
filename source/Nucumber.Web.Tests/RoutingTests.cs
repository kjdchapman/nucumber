using System.Web.Routing;
using NUnit.Framework;
using MvcContrib.TestHelper;
using Nucumber.Web.Controllers;

namespace Nucumber.Web.Tests
{
    [TestFixture]
    public class RoutingTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            RouteTable.Routes.Clear();
            new RouteConfigurator().RegisterRoutes(RouteTable.Routes); 
        }

        [Test]
        public void Default_route_should_hit_example_controller()
        {
            "~/".ShouldMapTo<ExampleController>(controller => controller.Index());
        }
    }
}
