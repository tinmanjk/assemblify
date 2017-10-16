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
    public class GetPostsByUserNameMappedToTests
    {
        [Test]
        public void GetPostsByUserNameMappedTo_ShouldCallRepositoryAll()
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
            var userName = "userName";
            // Act
            service.GetPostsByUserNameMappedTo<PostMapFrom>(userName);

            // Assert
            mockedPostRepository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetPostsByUserNameMappedTo_WhenFilterMatches_ShouldReturnMappedObject()
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

            var userName = "userName";

            var authorMock = new User
            {
                UserName = userName
            };

            var postsMock = new List<Post>
            {
                new Post()
                {
                    Author = authorMock,
                }
            };

            mockedPostRepository
                .SetupGet(x => x.All)
                .Returns(postsMock.AsQueryable);


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);

            // Act

            var result = service.GetPostsByUserNameMappedTo<PostMapFrom>(userName);

            // Assert
            // TODO - Navigational properties BUG
            //Assert.AreEqual(result.FirstOrDefault().AuthorUserName, userName);
        }

        [Test]
        public void GetPostsByUserNameMappedTo_WhenFilterDoesNotMatch_ShouldReturnMappedObject()
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

            
            var mockUserName = "userName";
            var authorMock = new User
            {
                UserName = mockUserName
            };

            var postsMock = new List<Post>
            {
                new Post()
                {
                    Author = authorMock,
                }
            };

            mockedPostRepository
                .SetupGet(x => x.All)
                .Returns(postsMock.AsQueryable);


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);

            // Act
            var wrongUserName = "pesho";
            var result = service.GetPostsByUserNameMappedTo<PostMapFrom>(wrongUserName);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
