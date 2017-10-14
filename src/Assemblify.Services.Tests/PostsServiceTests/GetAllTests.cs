using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Infrastructure.Factories;
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
    public class GetAllTests
    {
        [Test]
        public void TestGetAll_ShouldCallRepositoryAll()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUsersService>();

            var service = new PostsService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object);

            // Act
            service.GetAll();

            // Assert
            mockedPostRepository.Verify(r => r.All, Times.Once);
        }
    }
}
