using System.Web.Mvc;
using System.Web.Routing;

namespace QuasarLight.UI
{
    public class RouteConfig
    {
        private static readonly string[] Courses = { "english", "french", "italian", "spanish", "german", "portuguese" };

        private static void MapCourses(RouteCollection routes)
        {
            foreach (var course in Courses)
            {
                routes.MapRoute(
                    name: course,
                    url: course,
                    defaults: new { controller = "Course", action = course });
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            MapCourses(routes);

            routes.MapRoute(
                name: "teachers",
                url: "teachers",
                defaults: new { controller = "Teacher", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "how-it-works",
                url: "how-it-works",
                defaults: new { controller = "Home", action = "OrderLesson", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "order-lessons",
                url: "order-lessons",
                defaults: new { controller = "Home", action = "Costs", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "courses",
                url: "courses",
                defaults: new { controller = "Home", action = "Courses", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "feedback",
                url: "feedback",
                defaults: new { controller = "Home", action = "Testimonials", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "PageNotFound",
                url: "{url}",
                defaults: new { controller = "Home", action = "PageNotFound", id = UrlParameter.Optional });
        }

    }
}