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
    public class CreatePostTests
    {
        [Test]
        public void CreatePost_ModelStateIsNotValid_ShouldReturnViewWithModel()
        {
            // Arrange
            var mockedLogService = new Mock<IPostService>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapper>();

            var model = new PostCreateViewModel();

            var controller = new PostController(mockedLogService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            controller.ModelState.AddModelError("key", "value");

            // Act, Assert
            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRenderDefaultView()
                .WithModel<PostCreateViewModel>(m => Assert.AreSame(model, m));
        }
    }
}
