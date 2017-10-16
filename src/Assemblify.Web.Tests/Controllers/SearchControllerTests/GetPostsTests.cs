using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
using Assemblify.Web.Areas.Administration.Controllers;
using Assemblify.Web.Areas.Administration.ViewModels.Post;
using Assemblify.Web.Controllers;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.ViewModels.Home;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Assemblify.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class GetPostsTests
    {
        [Test]
        public void GetPosts_ShouldReturnViewResult()
        {
            // Arrange
            var mockedPostService = new Mock<IPostService>();

            var viewModel = new List<PostViewModel>()
            {
                new PostViewModel()
                {
                    Title = "title",
                    Content = "content",
                    AuthorUserName = "username",
                    Id = Guid.NewGuid(),
                    PostedOn = DateTime.Now
                }
            };

            var searchTerm = "searchterm";

            mockedPostService.Setup(x => x.GetPostsFilteredForTitleOrContent<PostViewModel>(searchTerm))
                .Returns(viewModel);

            var controller = new SearchController(mockedPostService.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.GetPosts(searchTerm))
                .ShouldRenderPartialView("_PostSearchListPartial")
                .WithModel<IEnumerable<PostViewModel>>(viewModel);
        }
    }
}
