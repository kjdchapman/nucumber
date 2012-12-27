using System.IO;
using System.Web.Mvc;

namespace Nucumber.Web.Controllers
{
    public class NucumberController : Controller
    {
        public const string FeatureFileFolder = "/Features/";

        public ActionResult Index()
        {
            return Json("You have navigated to features correctly",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string id)
        {
            return Json(
                string.Format("You have navigated to the feature file for {0} correctly", id), 
                JsonRequestBehavior.AllowGet
            );
        }
    }
}
