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
    public class AddToRoleTests
    {
        [Test]
        public void TestAddToRole_ShouldCallUserManagerAddToRole()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            string userId = "userId";
            string role = "user";

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            authenticationProvider.AddToRole(userId, role);

            // Assert
            mockedUserManager.Verify(m => m.AddToRoleAsync(userId, role), Times.Once);
        }
    }
}
