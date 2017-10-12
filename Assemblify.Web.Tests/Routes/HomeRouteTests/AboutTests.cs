using Assemblify.Web.Controllers;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Routes;
using Assemblify.Web.Routes.Contraints;
using Moq;
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
            var constraintsFactoryMock = new Mock<IConstraintsFactory>();
            var cachingProviderMock = new Mock<ICachingProvider>();
            var userNameConstraintMock = new UserNameConstraintMock(cachingProviderMock.Object);

            constraintsFactoryMock
                .Setup(x => x.CreateUserNameConstraint(cachingProviderMock.Object))
                .Returns(userNameConstraintMock);

            var routeCollection = new RouteCollection();
            var routeConfig = new RouteConfig(constraintsFactoryMock.Object, cachingProviderMock.Object);

            routeConfig.RegisterRoutes(routeCollection);

            // Act && Assert
            routeCollection.ShouldMap(url).To<HomeController>(c => c.About());
        }
    }
}
