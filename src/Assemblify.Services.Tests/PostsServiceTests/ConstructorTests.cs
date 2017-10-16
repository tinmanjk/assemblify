using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Factories;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Services;
using Assemblify.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Services.Tests.PostsServiceTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_WithPassedNullPostRepository_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(null,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void Constructor_WithPassedNullSaveContext_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(mockedPostRepository.Object,
                null,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void Constructor_WithPassedNullPostFactory_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                null,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void Constructor_WithPassedNullUserService_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                null,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void Constructor_WithPassedNullDateTimeProvider_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                null));
        }

        [Test]
        public void Constructor_WithPassedEverythingCorrectly_ShouldInitializeCorrectly()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            // Act
            var postService = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Assert
            Assert.IsNotNull(postService);
        }
    }
}
