using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Tests.AuthenticationProviderTests
{
    [TestFixture]
    public class CurrentUserUsernameTests
    {
        [Test]
        public void CurrentUserUsername_ShouldCallHttpContextProviderCurrentIdentity()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedIdentity = new Mock<IIdentity>();

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.CurrentIdentity).Returns(mockedIdentity.Object);

            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider.Object);

            // Act
            var result = authenticationProvider.CurrentUserUsername;

            // Assert
            mockedHttpContextProvider.Verify(p => p.CurrentIdentity, Times.Once);
        }
    }
}
