using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Incoding.MvcContrib;

namespace QuasarLight.UI.Controllers
{
    public class ProductController : IncControllerBase
    {
        public ActionResult Products()
        {
            return View();
        }
    }
}