using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nucumber.Parsing;

namespace Nucumber.Controllers
{
    public class NucumberController : Controller
    {
        private IEnumerable<Gherkin> _features = FeatureStore.GetInstance();

        public ActionResult Index()
        {
            return Json("You have navigated to features correctly", JsonRequestBehavior.AllowGet);
        }

        /// <param name="id">The name of the feature file</param>
        public ActionResult Details(string id)
        {
            var feature = _features.First();

            return View("Details");
        }
    }
}
