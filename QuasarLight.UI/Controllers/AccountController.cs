﻿using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using QuasarLight.Data.Model.DataModel;
using QuasarLight.Data.Model.ViewModel;
using QuasarLight.UI.Models;
using Microsoft.AspNet.Identity.Owin;

namespace QuasarLight.UI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private UserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /*[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }*/

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AuthorizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Login,
                    Email = model.Email,
                    Name = model.Name,
                    LastName = model.LastName,
                    TokenValidTo = DateTime.Today.Add(TimeSpan.FromDays(10))
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                model.ModelState = ModelState;
            }
            model.Password = "";
            model.ConfirmPassword = "";
            // If we got this far, something failed, redisplay form
            //return RedirectToRoute("login#register", model);
            return RedirectToAction("Login", "Account", model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthorizeViewModel model)
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? RedirectToAction("PageNotFound", "Error") : RedirectToAction("ResetPassword", "Account");
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}