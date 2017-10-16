using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.ViewModels.Home;
using Assemblify.Web.ViewModels.Search;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService postsService;
        private readonly IMapper mapper;
        private readonly IHttpCachingProvider cachingProvider;

        public SearchController(IPostService postsService,
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
            return this.View(new PostSearchViewModel()
            {
                FoundPosts = new List<PostViewModel>()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PostSearchViewModel model)
        {
            if (model.SearchTerm != null)
            {
                model.FoundPosts = this.postsService
                    .GetAllMappedTo<PostViewModel>()
                    .Where(x =>
                            x.Title.ToLower().Contains(model.SearchTerm.ToLower()))
                    .OrderBy(x => x.Title)
                    .ToList();
            }
            else
            {
                model.FoundPosts = new List<PostViewModel>();
            }

            return this.View(model);
        }
    }
}