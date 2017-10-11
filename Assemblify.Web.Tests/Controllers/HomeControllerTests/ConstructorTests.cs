using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Assemblify.Web.Providers.Contracts;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Tests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void ConstructorShould_Initialize_WithCorrectlyPassedParameters()
        {

            // Arrange
            var mockedPostsService = new Mock<IPostsService>();
            var mockedIMapper = new Mock<IMapper>();
            var mockedCachingProvider = new Mock<ICachingProvider>();

            // Act
            var controller = new HomeController(mockedPostsService.Object, mockedIMapper.Object, mockedCachingProvider.Object);
            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void TestConstructor_PassCachingProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostsService = new Mock<IPostsService>();
            var mockedIMapper = new Mock<IMapper>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(mockedPostsService.Object, mockedIMapper.Object, null));
        }
    }
}
