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
    public class SearchController : Controller
    {
        private readonly IPostService postsService;

        public SearchController(IPostService postsService)
        {
            if (postsService == null)
            {
                throw new ArgumentNullException(nameof(postsService));
            }

            this.postsService = postsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetPosts(string searchTerm)
        {
            var foundPosts = this.postsService
                    .GetAllMappedTo<PostViewModel>()
                    .Where(x =>
                          x.Title.ToLower().Contains(searchTerm.ToLower())
                          )
                    .OrderBy(x => x.Title)
                    .ToList();

            return this.PartialView("_PostSearchListPartial", foundPosts);
        }
    }
}