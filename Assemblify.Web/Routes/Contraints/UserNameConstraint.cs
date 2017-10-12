using Assemblify.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assemblify.Web.Routes.Contraints
{
    public class UserNameConstraint : IRouteConstraint
    {

        private IUsersService usersService;

        public UserNameConstraint()
        : this(DependencyResolver.Current.GetService<IUsersService>())
        {
        }

        public UserNameConstraint(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //List<string> users = new List<string>() { "admin", "pesho" };
            var users = this.usersService.GetAll();

            // Get the username from the url
            var username = values["username"].ToString().ToLower();
            // Check for a match (assumes case insensitive)
            return users.Any(x => x.UserName.ToLower() == username);
        }
    }
}