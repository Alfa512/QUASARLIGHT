using System.Web.Mvc;

namespace QuasarLight.UI.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Products()
        {
            return View();
        }
    }
}