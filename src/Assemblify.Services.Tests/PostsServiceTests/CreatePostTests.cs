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
    public class CreatePostTests
    {
        [Test]
        public void CreatePost_ShouldCallUsersServiceGetById()
        {
            // Arrange
            var title = "title";
            var content = "content";
            var userId = "userId";

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
            postService.CreatePost(title, content, userId);

            // Assert
            mockedUsersService.Verify(r => r.GetUserById(userId), Times.Once);
        }

        [Test]
        public void Edit_UserServiceReturnsUser_ShouldCreatePost()
        {
            // Arrange
            var title = "title";
            var content = "content";
            var userId = "userId";

            var authorMock = new User()
            {
                Id = userId
            };

            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(x => x.GetUserById(userId)).
                Returns(authorMock);

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedPost = new Post
            {
                Title = title,
                Content = content
            };

            mockedPostFactory.Setup(x => x.CreatePost(title, content, authorMock))
                .Returns(mockedPost);
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Act
            var result = postService.CreatePost(title, content, userId);

            // Assert
            Assert.AreEqual(result, mockedPost);
        }

        [Test]
        public void CreatePost_SaveContextThrowsException_ShouldReturnNull()
        {
            // Arrange
            var title = "title";
            var content = "content";
            var userId = "userId";

            var authorMock = new User()
            {
                Id = userId
            };

            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(x => x.GetUserById(userId)).
                Returns(authorMock);

            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            mockedSaveContext.Setup(x => x.Commit()).Throws<Exception>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedPost = new Post
            {
                Title = title,
                Content = content
            };

            mockedPostFactory.Setup(x => x.CreatePost(title, content, authorMock))
                .Returns(mockedPost);
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            // Act
            var result = postService.CreatePost(title, content, userId);

            // Assert
            Assert.IsNull(result);
        }

    }
}
