using Assemblify.Web.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Assemblify.Web.Providers.Tests.HttpCachingProviderTests
{
    [TestFixture]
    public class GetOrAddTests
    {
        [Test]
        public void GetOrADd_ShouldCallHttpContextProviderCurrentCache()
        {
            // Arrange
            var cache = HttpRuntime.Cache;
            string cacheKey = "cacheKey";
            int duration = 60;
            Func<string> func = () =>
            {
                return string.Empty;
            };

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.ContextCache).Returns(cache);

            var provider = new HttpCachingProvider(mockedHttpContextProvider.Object);

            // Act
            provider.GetOrAdd(cacheKey, func, duration);

            // Assert
            mockedHttpContextProvider.Verify(p => p.ContextCache, Times.Exactly(4));
        }

        public void GetOrAdd_ShouldReturnCorrectly(string key)
        {

            // Arrange
            var cache = HttpRuntime.Cache;
            string cacheKey = "cacheKey";
            var obj = new object();
            cache[cacheKey] = obj;
            int duration = 60;


            Func<string> func = () =>
            {
                return string.Empty;
            };

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.ContextCache).Returns(cache);

            var provider = new HttpCachingProvider(mockedHttpContextProvider.Object);

            // Act
            var result = provider.GetOrAdd(cacheKey, func, duration);
            // Assert
            Assert.AreSame(obj, result);
        }
    }
}
