using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Nucumber.Controllers
{
    public class NucumberController : Controller
    {
        private IEnumerable<FeatureFile> _featureStore;

        public ActionResult Index()
        {
            return Json("You have navigated to features correctly",JsonRequestBehavior.AllowGet);
        }

        /// <param name="id">The name of the feature file</param>
        public ActionResult Details(string id)
        {
            // TODO: Do this injection just the once rather than with each method call
            var featuresPath = Server.MapPath(Url.Content("~/Features/"));
            _featureStore = new FeatureStore(featuresPath);
            // </TODO>
            
            var featureFile = _featureStore.Single(f => f.Name == id);

            return Json(featureFile.RawText, JsonRequestBehavior.AllowGet);
        }
    }
}
