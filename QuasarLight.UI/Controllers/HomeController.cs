using System.Web.Mvc;

namespace QuasarLight.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Courses()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            return View();
        }

        //public ActionResult Costs()
        //{
        //    return View();
        //}

        //public ActionResult RequisitesRequestModal()
        //{
        //    return View("_RequisitesRequestModal");
        //}

        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}