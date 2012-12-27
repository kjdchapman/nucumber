using System.IO;
using System.Web.Mvc;

namespace Nucumber.Controllers
{
    public class NucumberController : Controller
    {
        public const string FeatureFileFolder = "/Features/";

        public ActionResult Index()
        {
            return Json("You have navigated to features correctly");
        }

        public string FeatureFile(string nameOfFeature)
        {
            var sourceFile = HttpContext.Server.MapPath(Path.Combine(FeatureFileFolder, "assets", "style", "release.css"));
            return "This feature file is at: " + new FilePathResult(sourceFile, "text/css").FileName;
        }
    }
}
