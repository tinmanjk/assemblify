using Assemblify.Data.Models;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Providers.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class SignInWithPasswordTests
    {
        [TestCase("email@email.com", "qwerty123", true, false)]
        public void SignInWithPassword_ShouldCallSignInManagerPasswordSignIn(string email, string password,
            bool remember, bool shouldLockout)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(mockedUserManager.Object, mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>()).Returns(mockedSignInManager.Object);

            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            authenticationProvider.SignInWithPassword(email, password, remember, shouldLockout);

            // Assert
            mockedSignInManager.Verify(m => m.PasswordSignInAsync(email, password, remember, shouldLockout));
        }

        [TestCase("email@email.com", "password", true, false, SignInStatus.Success)]
        [TestCase("email@email.com", "password", true, false, SignInStatus.Failure)]
        [TestCase("email@email.com", "password", true, false, SignInStatus.RequiresVerification)]
        [TestCase("email@email.com", "password", true, false, SignInStatus.LockedOut)]
        public void SignInWithPassword_ShouldReturnCorrectly(string email, string password,
            bool remember, bool shouldLockout, SignInStatus signInStatus)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(mockedUserManager.Object, mockedAuthenticationManager.Object);
            mockedSignInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>())).ReturnsAsync(signInStatus);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>()).Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>()).Returns(mockedSignInManager.Object);

            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            var result = authenticationProvider.SignInWithPassword(email, password, remember, shouldLockout);

            // Assert
            Assert.AreEqual(signInStatus, result);
        }
    }
}
