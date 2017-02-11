using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QuasarLight.Domain.Infrastructure;

namespace QuasarLight.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
                
            //FluentValidationModelValidatorProvider.Configure();

            Bootstrapper.Start();
            //var start = new Startup();
            //start.Configuration();
        }

        void Application_Error(object sender, EventArgs e)
        {
            var exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }
    }
}