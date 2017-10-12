using Assemblify.Services.Contracts;
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

namespace Assemblify.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        [Test]
        public void IndexShouldWorkCorrectly()
        {

            var mockedPostsService = new Mock<IPostsService>();
            var mockedIMapper = new Mock<IMapper>();
            var mockedCachingProvider = new Mock<ICachingProvider>();

            // Act

            var postViewModels = new List<PostViewModel>
            {
                new PostViewModel
                {
                    Title="Post 1",
                    Content = "Content 1",
                    AuthorEmail = "admin@admin.com",
                    PostedOn = DateTime.Now,
                    Id = Guid.NewGuid()
                }
            };

            mockedPostsService.Setup(x => x.GetAllMappedTo<PostViewModel>())
                .Returns(postViewModels);

            var controller = new HomeController(mockedPostsService.Object, mockedIMapper.Object, mockedCachingProvider.Object);

            mockedCachingProvider.Setup(x => x.Get(It.IsAny<string>(),
                                        It.IsAny<Func<IEnumerable<PostViewModel>>>(), 
                                        It.IsAny<int>()))
                                   .Returns((Func<PostViewModel> captured) => { captured(); return postViewModels;
                                   });

        controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomeViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(postViewModels, viewModel.Posts);
                    }).AndNoModelErrors();

        }
    }
}
