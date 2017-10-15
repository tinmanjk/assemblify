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
    public class EditGetTests
    {
        [Test]
        public void EditGet_WithNullId_ShouldReturnCorrectHttpStatusCode()
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
                .WithCallTo(c => c.Edit(postId))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
                
        }

        [Test]
        public void EditGet_WithCorrectIdAndNoFoundPost_ShouldReturnHttpNotFound()
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
                .WithCallTo(c => c.Edit(postId))
                .ValidateActionReturnType<HttpNotFoundResult>();

        }

        [Test]
        public void EditGet_WithCorrectIdAndReturnedPost_ShouldReturnCorrectViewWithModel()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var postId = Guid.NewGuid();

            var post = new Post();
            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(x => x.GetPostByIdAndDeleted(postId))
                .Returns(post);

            var postEditViewModel = new PostEditViewModel()
            {
                Title = "title",
                Content = "content",
                Id = postId,
                IsDeleted = false
            };

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<PostEditViewModel>(post))
                .Returns(postEditViewModel);

            var controller = new PostController(mockedPostService.Object, mockedAuthenticationProvider.Object,
               mockedMapper.Object);

            // Act, Assert
            controller
                .WithCallTo(c => c.Edit(postId))
                .ShouldRenderDefaultView()
                .WithModel<PostEditViewModel>(m => Assert.AreSame(postEditViewModel, m));
        }
    }
}
