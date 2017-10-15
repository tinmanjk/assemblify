using Assemblify.Common;
using Assemblify.Data.Models;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Assemblify.Web.Tests.Controllers.Administration.PostControllerTests
{
    [TestFixture]
    public class EditPostTests
    {
        [Test]
        public void EditPost_ModelStateIsNotValid_ShouldReturnViewWithModel()
        {
            // Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapper>();

            var model = new PostEditViewModel();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            controller.ModelState.AddModelError("key", "value");

            // Act, Assert
            controller
                .WithCallTo(c => c.Edit(model))
                .ShouldRenderDefaultView()
                .WithModel<PostEditViewModel>(m => Assert.AreSame(model, m));
        }

        [Test]
        public void EditPost_ServiceWorkingCorrectly_ShouldRedirectCorrectly()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var post = new Post { Id = postId };

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s => s.Edit(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(post);

            var userId = Guid.NewGuid();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var content = "content";
            var title = "title";

            var mockedMapper = new Mock<IMapper>();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
                 mockedMapper.Object);

            var model = new PostEditViewModel { Content = content, Title = title };

            // Act, Assert
            controller
                .WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo((PostController pc) => pc.Index());
        }

        [Test]
        public void EditPost_ServiceNotWorkingCorrectly_ShouldReturnModelWithErrors()
        {
            // Arrange
            Post post = null;

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s => s.Edit(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(post);

            var userId = Guid.NewGuid();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var content = "content";
            var title = "title";

            var mockedMapper = new Mock<IMapper>();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
                 mockedMapper.Object);

            var model = new PostEditViewModel { Content = content, Title = title };

            // Act, Assert
            controller
                .WithCallTo(c => c.Edit(model))
                .ShouldRenderDefaultView()
                .WithModel<PostEditViewModel>()
                .AndModelError(GlobalConstants.ErrorNotEditedPostKey);
        }
    }
}
