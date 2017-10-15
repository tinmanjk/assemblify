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
    public class DeleteGetTests
    {
        [Test]
        public void DeleteGet_WithNullId_ShouldReturnCorrectHttpStatusCode()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedMapper = new Mock<IMapper>();

            Guid? postId = null;

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Delete(postId))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
                
        }

        [Test]
        public void DeleteGet_WithCorrectIdAndNoFoundPost_ShouldReturnHttpNotFound()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedMapper = new Mock<IMapper>();

            Guid postId = Guid.NewGuid();
            Post post = null;
            mockedPostService.Setup(x => x.GetPostByIdAndDeleted(postId))
                .Returns(post);

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Delete(postId))
                .ValidateActionReturnType<HttpNotFoundResult>();

        }

        [Test]
        public void DeleteGet_WithCorrectIdAndReturnedPost_ShouldReturnCorrectViewWithModel()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var postId = Guid.NewGuid();

            var post = new Post();
            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(x => x.GetPostByIdAndDeleted(postId))
                .Returns(post);

            var postDeleteViewModel = new PostDeleteViewModel()
            {
                Title = "title",
                Content = "content",
                Id = postId,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
                AuthorUserName = "admin"

            };

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<PostDeleteViewModel>(post))
                .Returns(postDeleteViewModel);

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Delete(postId))
                .ShouldRenderDefaultView()
                .WithModel<PostDeleteViewModel>(m => Assert.AreSame(postDeleteViewModel, m));
        }
    }
}
