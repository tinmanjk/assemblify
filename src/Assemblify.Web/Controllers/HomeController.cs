using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.ViewModels.Home;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postsService;
        private readonly IHttpCachingProvider cachingProvider;

        public HomeController(IPostService postsService,
            IHttpCachingProvider cachingProvider)
        {
            if (postsService == null)
            {
                throw new ArgumentNullException(nameof(postsService));
            }

            if (cachingProvider == null)
            {
                throw new ArgumentNullException(nameof(cachingProvider));
            }

            this.postsService = postsService;
            this.cachingProvider = cachingProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var posts = this.cachingProvider
                .GetOrAdd("cachedPosts", () =>
                        this.postsService
                            .GetAllMappedTo<PostViewModel>(), 60 * 1);

            var model = new HomeViewModel()
            {
                Posts = posts
            };

            return View(model);
        }
    }
}