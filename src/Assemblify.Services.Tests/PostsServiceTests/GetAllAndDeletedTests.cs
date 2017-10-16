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
    public class GetAllAndDeletedTests
    {
        [Test]
        public void GetAllAndDeleted_ShouldCallRepositoryAll()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Act
            service.GetAllAndDeleted();

            // Assert
            mockedPostRepository.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void GetAllAndDeleted_ShouldReturnCorrectly()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post()
            }.AsQueryable();

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.AllAndDeleted).Returns(posts);

            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            var postService = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Act
            var result = postService.GetAllAndDeleted();

            // Assert
            Assert.AreEqual(posts, result);
        }
    }
}
