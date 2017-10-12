using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Services;
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

            var service = new PostsService(mockedPostRepository.Object,
                mockedSaveContext.Object);

            // Act
            service.GetAll();

            // Assert
            mockedPostRepository.Verify(r => r.All, Times.Once);
        }
    }
}
