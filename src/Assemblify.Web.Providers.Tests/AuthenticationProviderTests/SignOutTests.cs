﻿using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Microsoft.Owin;
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
    public class SignOutTests
    {
        [Test]
        public void TestSignOut_ShouldCallHttpContextProviderCurrentOwinContext()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedAuthManager = new Mock<IAuthenticationManager>();

            var mockedOwinContext = new Mock<IOwinContext>();
            mockedOwinContext.Setup(c => c.Authentication).Returns(mockedAuthManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.CurrentOwinContext).Returns(mockedOwinContext.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            provider.SignOut();

            // Assert
            mockedHttpContextProvider.Verify(p => p.CurrentOwinContext, Times.Once);
        }

        [Test]
        public void TestSignOut_ShouldCallAuthenticationManagerSignOut()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedAuthManager = new Mock<IAuthenticationManager>();

            var mockedOwinContext = new Mock<IOwinContext>();
            mockedOwinContext.Setup(c => c.Authentication).Returns(mockedAuthManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.CurrentOwinContext).Returns(mockedOwinContext.Object);

            var provider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            provider.SignOut();

            // Assert
            mockedAuthManager.Verify(m => m.SignOut(It.IsAny<string>()), Times.Once);
        }
    }
}
