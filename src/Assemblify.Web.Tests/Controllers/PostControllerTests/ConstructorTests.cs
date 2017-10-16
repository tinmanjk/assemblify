﻿using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Tests.Controllers.PostControllerTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_PassCachingProviderNull_ShouldThrowArgumentNullException()
        {
            //Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PostController(null));
        }

        [Test]
        public void ConstructorShould_Initialize_WithCorrectlyPassedParameters()
        {

            // Arrange
            var mockedPostsService = new Mock<IPostService>();

            // Act
            var controller = new PostController(mockedPostsService.Object);
            // Assert
            Assert.IsNotNull(controller);
        }
    }
}
