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
        private IHttpCachingProvider cachingProvider;
        private IUserService usersService;

        public RouteConfig(IConstraintsFactory constraintsFactory,
            IHttpCachingProvider cachingProvider,
            IUserService usersService)
        {
            this.constraintsFactory = constraintsFactory;
            this.cachingProvider = cachingProvider;
            this.usersService = usersService;

        }

        public RouteConfig()
            : this(DependencyResolver.Current.GetService<IConstraintsFactory>(),
                 DependencyResolver.Current.GetService<IHttpCachingProvider>(),
                 DependencyResolver.Current.GetService<IUserService>())
        {

        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "UserPosts",
                url: "{username}/posts/{postTitle}",
                defaults: new { controller = "Post", action = "PostsByUserName", postTitle = UrlParameter.Optional },
                constraints: new { username = constraintsFactory.CreateUserNameConstraint(this.cachingProvider, this.usersService) },
                namespaces: new[] { "Assemblify.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Assemblify.Web.Controllers" }
            );
        }


    }
}
