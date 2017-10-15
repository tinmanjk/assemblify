using Assemblify.Services.Contracts;
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
            string url = $"/home/index";
            var constraintsFactoryMock = new Mock<IConstraintsFactory>();
            var cachingProviderMock = new Mock<IHttpCachingProvider>();
            var usersServiceMock = new Mock<IUsersService>();
            var userNameConstraintMock = new UserNameConstraintMock();

            constraintsFactoryMock
                .Setup(x => x.CreateUserNameConstraint(cachingProviderMock.Object,usersServiceMock.Object))
                .Returns(userNameConstraintMock);

            var routeCollection = new RouteCollection();
            var routeConfig = new RouteConfig(constraintsFactoryMock.Object, cachingProviderMock.Object, usersServiceMock.Object);

            routeConfig.RegisterRoutes(routeCollection);

            // Act && Assert
            routeCollection.ShouldMap(url).To<HomeController>(c => c.Index());
        }
    }
}
