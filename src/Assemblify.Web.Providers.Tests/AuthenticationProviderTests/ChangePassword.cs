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
    public class ChangePassword
    {
        [Test]
        public void AddToRole_ShouldCallUserManagerChangePassword()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            string userId = "userId";
            string currentPassword = "currentPassword";
            string newPassword = "newPassword";


            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            authenticationProvider.ChangePassword(userId, currentPassword, newPassword);

            // Assert
            mockedUserManager.Verify(m => m.ChangePasswordAsync(userId, currentPassword, newPassword), Times.Once);
        }
    }
}
