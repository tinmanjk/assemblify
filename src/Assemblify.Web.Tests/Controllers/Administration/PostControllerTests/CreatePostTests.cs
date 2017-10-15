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
            var mockedPostService = new Mock<IPostService>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapper>();

            var model = new PostCreateViewModel();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            controller.ModelState.AddModelError("key", "value");

            // Act, Assert
            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRenderDefaultView()
                .WithModel<PostCreateViewModel>(m => Assert.AreSame(model, m));
        }

        [Test]
        public void CreatePost_DatabaseWorkingCorrectly_ShouldRedirectCorrectly()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var post = new Post { Id = postId };

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s => s.CreatePost(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(post);

            var userId = Guid.NewGuid(); // string moje da precaka

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(p => p.CurrentUserId).Returns(userId.ToString());

            var content = "content";
            var title = "title";

            var mockedMapper = new Mock<IMapper>();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
                 mockedMapper.Object);

            var model = new PostCreateViewModel { Content = content, Title = title };

            // Act, Assert
            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRedirectTo((PostController pc) => pc.Index());
        }

        [Test]
        public void CreatePost_DatabaseNotWorkingCorrectly_ShouldReturnModelWithErrors()
        {
            // Arrange
            Post post = null;

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s => s.CreatePost(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(post);

            var userId = Guid.NewGuid(); // string moje da precaka

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(p => p.CurrentUserId).Returns(userId.ToString());

            var content = "content";
            var title = "title";

            var mockedMapper = new Mock<IMapper>();

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
                 mockedMapper.Object);

            var model = new PostCreateViewModel { Content = content, Title = title };

            // Act, Assert
            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRenderDefaultView()
                .WithModel<PostCreateViewModel>()
                .AndModelError(GlobalConstants.ErrorNotCreatedPostKey);
        }

    }
}
