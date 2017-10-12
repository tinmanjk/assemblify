using Assemblify.Web.Controllers;
using MvcRouteTester;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Assemblify.Web.Tests.Routes.UserRouteTests
{
    [TestFixture]
    public class PostsTests
    {
        [Test]
        public void UserPostsRoute_ShouldWorkCorreclty()
        {
            // Arrange 
            string userName = "admin"; // works with constraints
            string postTitle = null;
            string Url = $"/{userName}/posts";

            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            // Act && Assert
            routeCollection.ShouldMap(Url).To<UserController>(c => c.Posts(userName, postTitle));
        }
    }
}
