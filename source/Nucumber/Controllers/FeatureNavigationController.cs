using System.Web.Mvc;

namespace Nucumber.Controllers
{
    public class FeatureNavigationController : Controller
    {
        public ActionResult Index()
        {
            return Json("You have navigated to features correctly",JsonRequestBehavior.AllowGet);
        }
    }
}
