using Assemblify.Services.Contracts;
using Assemblify.Web.Areas.Administration.Controllers;
using Assemblify.Web.Areas.Administration.ViewModels.Post;
using Assemblify.Web.Providers.Contracts;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Assemblify.Web.Tests.Controllers.Administration.PostControllerTests
{
    [TestFixture]
    public class CreateGetTests
    {
        [Test]
        public void CreateGet_ShouldReturnCorrectViewWithModel()
        {
            // Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapper>();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Create())
                .ShouldRenderDefaultView();
        }
    }
}
