﻿using Assemblify.Data.Models;
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
        private readonly IMapper mapper;
        private readonly IHttpCachingProvider cachingProvider;

        public HomeController(IPostService postsService,
            IMapper mapper,
            IHttpCachingProvider cachingProvider)
        {
            if (postsService == null)
            {
                throw new ArgumentNullException(nameof(postsService));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (cachingProvider == null)
            {
                throw new ArgumentNullException(nameof(cachingProvider));
            }

            this.postsService = postsService;
            this.mapper = mapper;
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