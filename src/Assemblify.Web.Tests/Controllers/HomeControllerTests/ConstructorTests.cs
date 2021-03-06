﻿using Assemblify.Services.Contracts;
using Assemblify.Web.Controllers;
using Assemblify.Web.Providers.Contracts;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Tests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void ConstructorShould_Initialize_WithCorrectlyPassedParameters()
        {

            // Arrange
            var mockedPostsService = new Mock<IPostService>();
            var mockedCachingProvider = new Mock<IHttpCachingProvider>();

            // Act
            var controller = new HomeController(mockedPostsService.Object, mockedCachingProvider.Object);
            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void Constructor_WithPassedCachingProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockedPostsService = new Mock<IPostService>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(mockedPostsService.Object, null));
        }
    }
}
