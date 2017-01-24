using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FluentNHibernate.Conventions;
using QuasarLight.UI.Controllers;

namespace QuasarLight.UI.Infrastructure
{
    public class AuthAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] != null && filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value.IsNotEmpty())
            {
                var coock = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(coock.Value);
                if (ticket != null && ticket.Expiration > DateTime.Now)
                {
                    if (filterContext.RouteData.Values["user"] == null)
                        filterContext.RouteData.Values.Add("user", ticket.UserData);
                    base.OnActionExecuting(filterContext);
                    return;
                }
            }
            filterContext.Controller.ControllerContext.Controller = new AccountController();
            HttpContext.Current.Response.Redirect("../Account/Login");
        }
    }
}