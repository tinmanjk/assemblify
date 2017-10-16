using Assemblify.Web.Areas.Administration.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Assemblify.Web.Tests.Controllers.Administration.ManageControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        [Test]
        public void TestIndex_ShouldReturnViewResult()
        {
            // Arrange
            var controller = new ManageController();

            // Act, Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
