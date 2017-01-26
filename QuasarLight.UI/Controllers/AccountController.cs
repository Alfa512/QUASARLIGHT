using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuasarLight.Data.Model.DataModel;
using QuasarLight.UI.Models;

namespace QuasarLight.UI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var teacher = Authenticate(model.Email, model.Password);

            if (teacher == null)
                return View(model);

            var ticket = new FormsAuthenticationTicket(1,
                teacher.Name,
                DateTime.Now, 
                DateTime.Now.AddMinutes(30),
                true,
                teacher.Email,
                FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            return /*teacher.IsAdmin == true ? RedirectToAction("Administration", "Admin") :*/ RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ""));
            return RedirectToAction("Index", "Home");
        }

        public User Authenticate(string name, string password)
        {
            var user = new User();
            return user;
            /*var teacher = dispatcher.Query(new GetEntitiesQuery<User>()).Find(r => r.Email == name);
            
            if (teacher == null)
                return null;

            return Crypto.VerifyHashedPassword(teacher.Password, password) ? teacher : null;*/
        }
    }
}