using Assemblify.Data.Models;
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

namespace Assemblify.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class IndexTests
    {

        [Test]
        public void Index_ShouldReturnViewResult()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();

            var controller = new SearchController(postServiceMock.Object);

                // Act, Assert
            controller
                    .WithCallTo(c => c.Index())
                    .ShouldRenderDefaultView();
        }
    }
}