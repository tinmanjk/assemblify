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
    public class GetAllUserNamesTest
    {
        [Test]
        public void GetAllUserNames_ShouldCallRepositoryAll()
        {
            // Arrange
            var mockedRepository = new Mock<IEfRepository<User>>();
            var mockedSaveContext = new Mock<ISaveContext>();

            var userService = new UserService(mockedRepository.Object, mockedSaveContext.Object);

            // Act
            userService.GetAllUserNames();

            // Assert
            mockedRepository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetAllUserNames_ShouldReturnCorrectResult()
        {
            // Arrange
            var users = new List<User>
            { new User()
                {
                    UserName="pesho"
                },
            new User()
                {
                    UserName="admin"
                }
            }
                .AsQueryable();

            var userNames = new List<string>
            {
                "pesho","admin"
            };

            var mockedRepository = new Mock<IEfRepository<User>>();
            mockedRepository.Setup(r => r.All).Returns(users);

            var mockedSaveContext = new Mock<ISaveContext>();

            var userService = new UserService(mockedRepository.Object, mockedSaveContext.Object);

            // Act
            var result = userService.GetAllUserNames();

            // Assert
            Assert.AreEqual(userNames, result);
        }
    }
}
