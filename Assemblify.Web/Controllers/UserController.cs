using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
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
        private readonly ICachingProvider cachingProvider;

        public UserController(IPostsService postsService,
            IMapper mapper,
            ICachingProvider cachingProvider)
        {
            this.postsService = postsService;
            this.mapper = mapper;
            this.cachingProvider = cachingProvider;
        }
    }
}