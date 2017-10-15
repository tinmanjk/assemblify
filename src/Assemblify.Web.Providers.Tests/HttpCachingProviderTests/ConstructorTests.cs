using Assemblify.Web.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Tests.HttpCachingProviderTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_WithPassedEverything_ShouldInitializeCorrectly()
        {
            // Arrange
            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();

            // Act
            var provider = new HttpCachingProvider(mockedHttpContextProvider.Object);

            // Assert
            Assert.IsNotNull(provider);
        }

        [Test]
        public void Constructor_ShouldBeInstanceOfICachingProvider()
        {
            // Arrange
            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();

            // Act
            var provider = new HttpCachingProvider(mockedHttpContextProvider.Object);

            // Assert
            Assert.IsInstanceOf<IHttpCachingProvider>(provider);
        }

        [Test]
        public void TestConstructor_PassHttpContextProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HttpCachingProvider(null));
        }
    }
}
