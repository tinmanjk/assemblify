using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Assemblify.Web.ViewModels.Post;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            // more information https://stackoverflow.com/questions/20364107/moq-lambda-expressions-as-parameters-and-evaluate-them-in-returns

            mockedPostService.Setup(x => x.GetFilteredByAndMappedTo<UserPostsViewModel>(It.IsAny<Expression<Func<Post, bool>>>()))
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
