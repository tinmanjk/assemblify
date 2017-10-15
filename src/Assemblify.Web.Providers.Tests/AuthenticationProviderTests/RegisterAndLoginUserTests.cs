using Assemblify.Data.Models;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Providers.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
    public class RegisterAndLoginUserTests
    {
        [Test]
        public void RegisterAndLoginUser_ShouldCallUserManagerCreate()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            string password = "password";
            bool isPersistent = true;
            bool rememberBrowser = true;

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult());

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            var user = new User();

            // Act
            provider.RegisterAndLoginUser(user, password, isPersistent, rememberBrowser);

            // Assert
            mockedUserManager.Verify(m => m.CreateAsync(user, password), Times.Once);
        }

        [Test]
        public void RegisterAndLoginUser_ReturnsSuccess_ShouldCallSignInManagerSignIn()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            string password = "password";
            bool isPersistent = true;
            bool rememberBrowser = true;

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(mockedUserManager.Object, mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>()).Returns(mockedSignInManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            var user = new User();

            // Act
            provider.RegisterAndLoginUser(user, password, isPersistent, rememberBrowser);

            // Assert
            mockedSignInManager.Verify(s => s.SignInAsync(user, isPersistent, rememberBrowser));
        }

        [Test]
        public void RegisterAndLoginUser_ShouldReturnCorrectly()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            string password = "password";
            bool isPersistent = true;
            bool rememberBrowser = true;

            var result = new IdentityResult();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(result);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            var user = new User();

            // Act
            var actualResult = provider.RegisterAndLoginUser(user, password, isPersistent, rememberBrowser);

            // Assert
            Assert.AreSame(result, actualResult);
        }
    }
}
