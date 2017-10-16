using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Factories;
using Assemblify.Infrastructure.Mapping;
using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Services.Contracts;
using Assemblify.Services.Tests.PostsServiceTests.Mocks;
using Assemblify.Web.Controllers;
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
    public class GetAllAndDeletedMappedToTests
    {

        [Test]
        public void GetAllAndDeletedMappedTo_ShouldCallRepositoryAll()
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

            // Act
            service.GetAllAndDeletedMappedTo<PostMapFrom>();

            // Assert
            mockedPostRepository.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void GetAllAndDeletedMappedTo_ShouldReturnMappedObject()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedMapFromPost = new PostMapFrom();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            var titleProperty = "zaglavie";

            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostMapFrom>();
            });

            var postsMock = new List<Post>
            {
                new Post()
                {
                    Content="Interesting content",
                    Title = titleProperty
                }
            };

            mockedPostRepository.SetupGet(x => x.AllAndDeleted).Returns(postsMock.AsQueryable);


            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object, mockedPostFactory.Object, mockedUsersService.Object, mockedDateTimeProvider.Object);

            // Act

            var result = service.GetAllAndDeletedMappedTo<PostMapFrom>();

            // Assert
            Assert.AreEqual(result.FirstOrDefault().Title, titleProperty);
        }


    }
}
