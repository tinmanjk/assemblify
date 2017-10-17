using Assemblify.Services.Contracts;
using Assemblify.Web.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostService postsService;

        public PostController(IPostService postsService)
        {
            if (postsService == null)
            {
                throw new ArgumentNullException(nameof(postsService));
            }
            this.postsService = postsService;
        }

        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult PostsByUserName(string username)
        {
            //var posts = this.postsService
            //                .GetPostsByUserNameMappedTo<UserPostsViewModel>(username);

            var posts = this.postsService
                .GetFilteredByAndMappedTo<UserPostsViewModel>(x => 
                    x.Author.UserName.ToLower() == username.ToLower());

            return View(posts);
        }
    }
}