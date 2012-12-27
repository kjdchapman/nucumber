using System;
using System.Web.Mvc;

namespace Nucumber.Web.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public string Details(int id)
        {
            return "This is a details view";
        }

        public string Create()
        {
            return "This is a create view";
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            throw new NotSupportedException();
        }
        
        public string Edit(int id)
        {
            return "This ia an edit view for " + id;
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            throw new NotSupportedException();
        }
 
        public ActionResult Delete(int id)
        {
            throw new NotSupportedException();
        }
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            throw new NotSupportedException();
        }
    }
}
