using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Factories;
using Assemblify.Infrastructure.Providers.Contracts;
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
    public class EditTests
    {
        [Test]
        public void Edit_ShouldCallRepositoryGetById()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
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
            postService.Edit(guid, title, content, isDeleted);

            // Assert
            mockedPostRepository.Verify(r => r.GetByIdAndDeleted(guid), Times.Once);
        }

        [Test]
        public void Edit_RepositoryReturnsNull_ShouldNotCallSaveContextCommit()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
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
            postService.Edit(guid, title, content, isDeleted);

            // Assert
            mockedSaveContext.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void Edit_RepositoryReturnsLog_ShouldSetPostContent()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var post = new Post();
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetByIdAndDeleted(It.IsAny<object>())).Returns(post);

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
            postService.Edit(guid, title, content, isDeleted);

            // Assert
            Assert.AreEqual(content, post.Content);
        }

        [Test]
        public void Edit_RepositoryReturnsLog_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var post = new Post();
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetByIdAndDeleted(It.IsAny<object>())).Returns(post);

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
            postService.Edit(guid, title, content, isDeleted);

            // Assert
            mockedPostRepository.Verify(r => r.Update(post), Times.Once);
        }

        [Test]
        public void Edit_RepositoryReturnsLog_ShouldCallSaveContextCommit()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var post = new Post();
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetByIdAndDeleted(It.IsAny<object>())).Returns(post);

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
            postService.Edit(guid, title, content, isDeleted);

            // Assert
            mockedSaveContext.Verify(r => r.Commit(), Times.Once);
        }

        [Test]
        public void Edit_SaveContextThrows_ShouldReturnNull()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var title = "title";
            var content = "content";
            var isDeleted = false;

            var post = new Post();
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetByIdAndDeleted(It.IsAny<object>())).Returns(post);

            var mockedSaveContext = new Mock<ISaveContext>();
            mockedSaveContext.Setup(x => x.Commit()).Throws<Exception>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Act
            var result = postService.Edit(guid, title, content, isDeleted);

            // Assert
            Assert.IsNull(result);
        }
    }
}
