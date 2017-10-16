using Assemblify.Data.Models;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Providers.Managers;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Tests.AuthenticationProviderTests
{
    [TestFixture]
    public class IsInRoleTests
    {
        [Test]
        public void IsInRole_ShouldCallUserManagerIsInRole()
        {
            // Arrange
            string userId = "userId";
            string role = "user";

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            provider.IsInRole(userId, role);

            // Assert
            mockedUserManager.Verify(m => m.IsInRoleAsync(userId, role), Times.Once);
        }

        [TestCase("userId", "user", true)]
        [TestCase("userId", "user", false)]
        public void IsInRole_ShouldReturnCorrectly(string userId, string role, bool isInRole)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(m => m.IsInRoleAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(isInRole);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            var result = provider.IsInRole(userId, role);

            // Assert
            Assert.AreEqual(isInRole, result);
        }

        [Test]
        public void IsInRole_UserIdIsNull_ShouldReturnFalse()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            string role = "user";

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            var result = provider.IsInRole(null, role);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
