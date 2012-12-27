using System.Web.Mvc;

namespace Nucumber.Controllers
{
    public class FeatureDetailontroller : Controller
    {
        public ActionResult Details(string id)
        {
            return Json(
                string.Format("You have navigated to the feature file for {0} correctly", id), 
                JsonRequestBehavior.AllowGet
            );
        }
    }
}
