using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_WithPassedRepositoryNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedSaveContext = new Mock<ISaveContext>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, mockedSaveContext.Object));
        }

        [Test]
        public void Constructor_WithPassedSaveContextNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedRepository = new Mock<IEfRepository<User>>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(mockedRepository.Object, null));
        }

        [Test]
        public void Constructor_WithPassEverythingCorrectly_ShouldNotThrow()
        {
            // Arrange
            var mockedRepository = new Mock<IEfRepository<User>>();
            var mockedSaveContext = new Mock<ISaveContext>();

            // Act, Assert
            Assert.DoesNotThrow(() => new UserService(mockedRepository.Object, mockedSaveContext.Object));
        }

        [Test]
        public void Constructor_WithPassEverythingCorrectly_ShouldInitializeCorrectly()
        {
            // Arrange
            var mockedRepository = new Mock<IEfRepository<User>>();
            var mockedSaveContext = new Mock<ISaveContext>();

            // Act
            var userService = new UserService(mockedRepository.Object, mockedSaveContext.Object);

            // Assert
            Assert.IsNotNull(userService);
        }
    }
}
