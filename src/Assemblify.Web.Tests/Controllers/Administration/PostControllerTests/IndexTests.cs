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
    public class IndexTests
    {
        [Test]
        public void TestIndex_ShouldReturnViewResult()
        {
            // Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapper>();

            var viewModel = new List<PostListViewModel>()
            {
                new PostListViewModel()
                {
                    Title = "title",
                    Content = "content",
                    AuthorUserName = "username",
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    DeletedOn = null
                }
            };

            mockedPostService.Setup(x => x.GetAllAndDeletedMappedTo<PostListViewModel>())
                .Returns(viewModel);

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<PostListViewModel>>(viewModel);
        }
    }
}
