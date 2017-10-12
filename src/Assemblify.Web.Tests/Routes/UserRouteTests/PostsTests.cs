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
            routeCollection.ShouldMap(Url).To<UserController>(c => c.Posts(userName, postTitle));
        }
    }
}
