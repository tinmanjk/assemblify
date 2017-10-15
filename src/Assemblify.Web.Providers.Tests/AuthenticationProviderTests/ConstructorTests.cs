using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using Microsoft.Owin;
using System.Security.Principal;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assemblify.Web.Providers.Tests.AuthenticationProviderTests.Mocks;

namespace Assemblify.Web.Providers.Tests.AuthenticationProviderTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_WithPassedDateTimeProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedHttpContextProvider = new MockedHttpContextProvider();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MockedAuthenticationProvider(null, mockedHttpContextProvider));
        }

        [Test]
        public void Constructor_WithPassedHttpContextProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MockedAuthenticationProvider(mockedDateTimeProvider.Object, null));
        }

        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedHttpContextProvider = new MockedHttpContextProvider();

            // Act
            var authenticationProvider = new AuthenticationProvider(mockedDateTimeProvider.Object, mockedHttpContextProvider);

            // Assert
            Assert.IsNotNull(authenticationProvider);
        }
    }
}
