using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Factories;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Services.Contracts;
using Assemblify.Services.Tests.PostsServiceTests.Mocks;
using AutoMapper;
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
    public class GetPostsFilteredForTitleOrContentMappedToTests
    {
        [Test]
        public void GetPostsFilteredForTitleOrContentMappedTo_ShouldCallRepositoryAll()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            var mockedMapFromPost = new PostMapFrom();


            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostMapFrom>();
            });

            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);
            var searchTerm = "searchTerm";
            // Act
            service.GetPostsFilteredForTitleOrContentMappedTo<PostMapFrom>(searchTerm);

            // Assert
            mockedPostRepository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetPostsFilteredForTitleOrContentMappedTo_WhenFilterMatches_ShouldReturnMappedObject()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedMapFromPost = new PostMapFrom();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostMapFrom>();
            });

            var searchTerm = "interesting";
            var content = "Interesting content";
            var titleProperty = "zaglavie";

            var postsMock = new List<Post>
            {
                new Post()
                {
                    Content=content,
                    Title = titleProperty
                }
            };

            mockedPostRepository
                .SetupGet(x => x.All)
                .Returns(postsMock.AsQueryable);


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);

            // Act

            var result = service.GetPostsFilteredForTitleOrContentMappedTo<PostMapFrom>(searchTerm);

            // Assert
            Assert.AreEqual(result.FirstOrDefault().Title, titleProperty);
        }

        [Test]
        public void GetPostsFilteredForTitleOrContentMappedTo_WhenFilterDoesNotMatch_ShouldReturnMappedObject()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedMapFromPost = new PostMapFrom();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostMapFrom>();
            });

            var searchTerm = "interestinggg";
            var content = "Interesting content";
            var titleProperty = "zaglavie";

            var postsMock = new List<Post>
            {
                new Post()
                {
                    Content=content,
                    Title = titleProperty
                }
            };

            mockedPostRepository
                .SetupGet(x => x.All)
                .Returns(postsMock.AsQueryable);


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);

            // Act

            var result = service.GetPostsFilteredForTitleOrContentMappedTo<PostMapFrom>(searchTerm);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
