using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nucumber.Parsing;

namespace Nucumber.Controllers
{
    public class NucumberController : Controller
    {
        private IEnumerable<Gherkin> _features;

        public ActionResult Index()
        {
            return Json("You have navigated to features correctly", JsonRequestBehavior.AllowGet);
        }

        /// <param name="id">The name of the feature file</param>
        public ActionResult Details(string id)
        {
            // TODO: Do this injection just the once rather than with each method call
            var featuresPath = Server.MapPath(Url.Content("~/Features/"));
            _features = new FeatureStore(featuresPath);
            // </TODO>

            var feature = _features.First();

            return Json(feature, JsonRequestBehavior.AllowGet);
        }
    }
}
