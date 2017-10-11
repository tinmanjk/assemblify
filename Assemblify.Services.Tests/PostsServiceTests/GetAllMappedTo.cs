using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Mapping;
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
    public class GetAllMappedTo
    {
        [Test]
        public void TestGetAll_ShouldCallRepositoryAll()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedMapFromPost = new PostMapFrom();

            var service = new PostsService(mockedPostRepository.Object,
                mockedSaveContext.Object);

            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostMapFrom>();
            });

            // Act

            service.GetAllMappedTo<PostMapFrom>();

            // Assert
            mockedPostRepository.Verify(r => r.All, Times.Once);
        }
    }
}
