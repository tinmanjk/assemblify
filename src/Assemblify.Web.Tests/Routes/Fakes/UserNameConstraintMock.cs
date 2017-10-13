using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assemblify.Web.Routes.Contraints
{
    public class UserNameConstraintMock : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {

            var usernames = new List<string>
            {
                "pesho",
                "admin"
            };

            // Get the username from the url
            var username = values["username"].ToString().ToLower();

            return usernames.Any(x => x.ToLower() == username);
        }
    }
}