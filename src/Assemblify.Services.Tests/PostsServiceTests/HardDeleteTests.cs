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
    public class HardDeleteTests
    {
        [Test]
        public void HardDelete_ShouldCallRepositoryGetByIdAndDeleted()
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
            service.HardDelete(id);

            // Assert
            mockedPostRepository.Verify(r => r.GetByIdAndDeleted(id), Times.Once);
        }
        [Test]
        public void HardDelete_SaveContextShouldCommitIfFoundInRepository()
        {
            // Arrange
            var mockedPostRepository = new Mock<IEfRepository<Post>>();
            var mockedSaveContext = new Mock<ISaveContext>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedUsersService = new Mock<IUserService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var post = new Mock<Post>();
            mockedPostRepository.Setup(x => x.GetByIdAndDeleted(It.IsAny<object>()))
                .Returns(post.Object);

            var service = new PostService(mockedPostRepository.Object,
                mockedSaveContext.Object,
                mockedPostFactory.Object,
                mockedUsersService.Object,
                mockedDateTimeProvider.Object);

            var id = Guid.NewGuid();
            // Act
            service.HardDelete(id);

            // Assert
            mockedSaveContext.Verify(r => r.Commit(), Times.Once);
        }
    }
}
