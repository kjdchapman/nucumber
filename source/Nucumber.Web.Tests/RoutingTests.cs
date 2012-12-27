using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using MvcContrib.TestHelper;
using Nucumber.Web;
using Nucumber.Web.Controllers;

namespace Nucumber.Tests
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
        public void Nucumber_index_route_should_hit_nucumber_controller()
        {
            "~/features/index".ShouldMapTo<NucumberController>(controller => controller.Index());
        }

        [Test]
        public void Nucumber_route_should_hit_nucumber_controller()
        {
            "~/features/details/example".ShouldMapTo<NucumberController>(controller => controller.Details("example"));
        }

        [Test]
        public void Default_route_should_hit_example_controller()
        {
            "~/".ShouldMapTo<ExampleController>(controller => controller.Index());
        }
    }
}
