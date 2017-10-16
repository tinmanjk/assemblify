using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Assemblify.Web.Tests.Controllers.PostControllerTests
{
    [TestFixture]
    public class CreateTests
    {

        [Test]
        public void Create_ShouldReturnViewResult()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();

            var controller = new PostController(postServiceMock.Object);

            // Act, Assert
            controller
                    .WithCallTo(c => c.Create())
                    .ShouldRenderDefaultView();
        }
    }
}
