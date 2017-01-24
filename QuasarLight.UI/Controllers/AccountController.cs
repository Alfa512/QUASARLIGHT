using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Incoding.CQRS;
using Incoding.MvcContrib;
using QuasarLight.Domain.Models;
using QuasarLight.UI.Models;

namespace QuasarLight.UI.Controllers
{
    public class AccountController : IncControllerBase
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
                teacher.FullName,
                DateTime.Now, 
                DateTime.Now.AddMinutes(30),
                true,
                teacher.Email,
                FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            return teacher.IsAdmin == true ? RedirectToAction("Administration", "Admin") : RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ""));
            return RedirectToAction("Index", "Home");
        }

        public Teacher Authenticate(string name, string password)
        {
            var teacher = dispatcher.Query(new GetEntitiesQuery<Teacher>()).Find(r => r.Email == name);
            
            if (teacher == null)
                return null;

            return Crypto.VerifyHashedPassword(teacher.Password, password) ? teacher : null;
        }
    }
}