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
    public class EditGetTests
    {
        [Test]
        public void CreateGet_WithCorredIdAndReturnedPost_ShouldReturnCorrectViewWithModel()
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
