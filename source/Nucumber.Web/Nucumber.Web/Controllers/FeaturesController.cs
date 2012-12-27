using System;
using System.Web.Mvc;

namespace Nucumber.Web.Controllers
{
    public class FeaturesController : Controller
    {
        public ActionResult Index()
        {
            throw new Exception("Controller in base web project has been instantiated to service this request.");
        }

    }
}
