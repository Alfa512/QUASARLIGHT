using System.Web.Mvc;
using Incoding.MvcContrib;

namespace QuasarLight.UI.Controllers
{
    public class HomeController : IncControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Courses()
        //{
        //    return View();
        //}

        //public ActionResult OrderLesson()
        //{
        //    return View();
        //}

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