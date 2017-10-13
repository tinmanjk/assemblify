using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Routes;
using Assemblify.Web.Routes.Contraints;
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
        private IConstraintsFactory constraintsFactory;
        private ICachingProvider cachingProvider;
        private IUsersService usersService;

        public RouteConfig(IConstraintsFactory constraintsFactory,
            ICachingProvider cachingProvider,
            IUsersService usersService)
        {
            this.constraintsFactory = constraintsFactory;
            this.cachingProvider = cachingProvider;
            this.usersService = usersService;

        }

        public RouteConfig()
            :this(DependencyResolver.Current.GetService<IConstraintsFactory>(), 
                 DependencyResolver.Current.GetService<ICachingProvider>(),
                 DependencyResolver.Current.GetService<IUsersService>())
        {

        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "UserPosts",
                url: "{username}/{action}/{postTitle}",
                defaults: new { controller = "User", action = "UserProfile", postTitle = UrlParameter.Optional },
                constraints: new { username = constraintsFactory.CreateUserNameConstraint(this.cachingProvider,this.usersService) }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
