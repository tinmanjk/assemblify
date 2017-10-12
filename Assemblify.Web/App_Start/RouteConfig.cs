using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assemblify.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "UserPosts",
                url: "{username}/{action}/{postTitle}",
                defaults: new { controller = "User", action = "UserProfile", postTitle = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );




        }

        public class UserNameConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                List<string> users = new List<string>() { "admin", "pesho" };
                // Get the username from the url
                var username = values["username"].ToString().ToLower();
                // Check for a match (assumes case insensitive)
                return users.Any(x => x.ToLower() == username);
            }
        }
    }
}
