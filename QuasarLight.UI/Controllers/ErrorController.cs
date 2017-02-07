using System.Web.Mvc;

namespace QuasarLight.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View("PageNotFound");
        }

        public ActionResult ServerError()
        {
            return View("ServerError");
        }
    }
}