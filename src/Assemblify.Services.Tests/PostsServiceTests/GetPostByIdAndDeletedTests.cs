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
    public class GetPostByIdAndDeletedTests
    {
        [Test]
        public void GetPostByIdAndDeleted_ShouldCallRepositoryGetByIdAndDeleted()
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

            var id = Guid.NewGuid();
            // Act
            service.GetPostByIdAndDeleted(id);

            // Assert
            mockedPostRepository.Verify(r => r.GetByIdAndDeleted(id), Times.Once);
        }

        [Test]
        public void GetPostByIdAndDeleted_RepositoryReturnsLog_ShouldReturnCorrectly()
        {
            // Arrange
            var post = new Mock<Post>();
            var id = Guid.NewGuid();

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetByIdAndDeleted(It.IsAny<object>()))
                .Returns(post.Object);

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
            var result = postService.GetPostByIdAndDeleted(id);

            // Assert
            Assert.AreSame(post.Object, result);
        }
    }
}
