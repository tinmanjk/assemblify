using Assemblify.Web.Controllers;
using MvcRouteTester;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Assemblify.Web.Tests.Routes.HomeRouteTests
{
    [TestFixture]
    public class HomeRouteTests
    {
        [Test]
        public void HomeAboutRoute_ShouldWorkCorreclty()
        {
            // Arrange 
            string url = $"/home/about";

            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            // Act && Assert
            routeCollection.ShouldMap(url).To<HomeController>(c => c.About());
        }
    }
}
