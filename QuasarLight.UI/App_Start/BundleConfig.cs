using System.Web.Optimization;

namespace QuasarLight.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/plugins.css",
                      "~/Content/main.css",
                      "~/Content/main-s.css",
                      "~/Content/themes.css",
                      "~/Content/site.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/jquery-1.11.3").Include("~/Scripts/jquery-1.11.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-1.11.3.min").Include("~/Scripts/jquery-1.11.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/TweenMax.min").Include("~/Scripts/TweenMax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-2.1.1.min").Include("~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ScrollToPlugin.min").Include("~/Scripts/ScrollToPlugin.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
            "~/Scripts/pages/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/jquery-1.11.3.js",
                "~/Scripts/TweenMax.min.js",
                "~/Scripts/ScrollToPlugin.min.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/jquery.the-modal.js",
                "~/Scripts/wow.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/vendors/trianglify.min.js",
                "~/Scripts/Card-circle.js",
                "~/Scripts/admin.services.js",
                "~/Scripts/global.services.js",
                "~/Scripts/SmoothScroll.js",
                "~/Scripts/_references.js",
                "~/Scripts/jquery.dlmenu.js",
                "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/layout.top").Include(
                "~/Scripts/jquery-1.11.3.js",
                "~/Scripts/attrchange.js",
                "~/Scripts/wow.min.js",
                "~/Scripts/TweenMax.min.js",
                "~/Scripts/ScrollToPlugin.min.js",
                "~/Scripts/angular.js",
                "~/Scripts/yandex.watch.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Scripts/modernizr.custom.js",
                "~/Scripts/jquery.the-modal.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.nicescroll.min.js",
                "~/Scripts/vendors/trianglify.min.js",
                "~/Scripts/Card-circle.js",
                "~/Scripts/course.animation.js",
                "~/Scripts/_references.js",
                "~/bundles/jqueryval.js",
                "~/Scripts/jquery.dlmenu.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/layout.bottom").Include(
                "~/Scripts/plugins.js",
                "~/Scripts/app.js",
                "~/Scripts/global.services.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                            "~/Scripts/index.js"
                            ));

            bundles.Add(new ScriptBundle("~/bundles/courses").Include(
                "~/Scripts/vendors/cash.min.js",
                "~/Scripts/attrchange.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/costs").Include(
                "~/Scripts/payment.services.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/teacher").Include(
                "~/Scripts/attrchange.js",
                "~/Scripts/teachers.services.js"
                 ));

            BundleTable.EnableOptimizations = false;
        }
    }
}