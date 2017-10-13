﻿using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.ViewModels.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;
        private readonly IHttpCachingProvider cachingProvider;

        public UserController(IPostsService postsService,
            IMapper mapper,
            IHttpCachingProvider cachingProvider)
        {
            this.postsService = postsService;
            this.mapper = mapper;
            this.cachingProvider = cachingProvider;
        }

        public ActionResult Posts(string username, string postTitle)
        {
            var posts = this.postsService
                            .GetPostsByUserNameMappedTo<UserPostsViewModel>(username);
            return View(posts);
        }

        public ActionResult UserProfile(string username, string postTitle)
        {
            return Content("Profile of" + username);
        }
    }
}