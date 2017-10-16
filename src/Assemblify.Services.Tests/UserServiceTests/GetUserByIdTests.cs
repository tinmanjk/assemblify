using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetUserByIdTests
    {
        [Test]
        public void GetUserById_ShouldCallRepositoryGetById()
        {
            // Arrange
            var mockedRepository = new Mock<IEfRepository<User>>();
            var mockedSaveContext = new Mock<ISaveContext>();

            var userService = new UserService(mockedRepository.Object, mockedSaveContext.Object);

            var id = "username";
            // Act
            userService.GetUserById(id);

            // Assert
            mockedRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnCorrectResult()
        {
            // Arrange
            var mockedUser = new Mock<User>();

            var mockedRepository = new Mock<IEfRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedUser.Object);

            var mockedSaveContext = new Mock<ISaveContext>();

            var userService = new UserService(mockedRepository.Object, mockedSaveContext.Object);
            var id = "username";
            
            // Act
            var result = userService.GetUserById(id);

            // Assert
            Assert.AreSame(mockedUser.Object, result);
        }
    }
}
