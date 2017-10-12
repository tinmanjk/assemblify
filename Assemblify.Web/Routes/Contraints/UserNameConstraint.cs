﻿using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
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

        private ICachingProvider cachingProvider;
        //private IUsersService usersService;

        public UserNameConstraint()
        : this(DependencyResolver.Current.GetService<ICachingProvider>())
        {
        }

        public UserNameConstraint(ICachingProvider cachingProvider)
        {
            this.cachingProvider = cachingProvider;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {

            var usernames = this.cachingProvider.GetUserNames();

            // Get the username from the url
            var username = values["username"].ToString().ToLower();
            // Check for a match (assumes case insensitive)
            return usernames.Any(x => x.ToLower() == username);
        }
    }
}