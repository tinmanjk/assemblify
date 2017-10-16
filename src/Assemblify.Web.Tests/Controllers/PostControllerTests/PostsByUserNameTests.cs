using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Assemblify.Web.ViewModels.Post;
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
    public class PostsByUserNameTests
    {
        [Test]
        public void PostsByUserName_ShouldReturnViewResult()
        {
            // Arrange
            var mockedPostService = new Mock<IPostService>();

            var viewModel = new List<UserPostsViewModel>()
            {
                new UserPostsViewModel()
                {
                    Title = "title",
                    Content = "content",
                    PostedOn = DateTime.Now
                }
            };

            var userName = "userName";

            mockedPostService.Setup(x => x.GetPostsByUserNameMappedTo<UserPostsViewModel>(userName))
                .Returns(viewModel);

            var controller = new PostController(mockedPostService.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.PostsByUserName(userName))
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<UserPostsViewModel>>(viewModel);
        }
    }
}
