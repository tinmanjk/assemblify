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
        public ActionResult GetFiltered(int pageSize)
        {
            var posts = this.postsService
                .GetAllMappedTo<PostViewModel>()
                .OrderBy(x => x.Title)
                .Take(pageSize)
                .ToList();

            return this.View(posts);
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            return this.View();
        }


        [HttpGet]
        public ActionResult Search()
        {
            return this.View(new PostSearchViewModel()
            {
                FoundPosts = new List<PostViewModel>()
            }
            );
        }

        [HttpPost]
        public ActionResult Search(PostSearchViewModel model)
        {
            if (model.SearchTerm != null)
            {
                model.FoundPosts = this.postsService
                    .GetAllMappedTo<PostViewModel>()
                    .Where(x =>
                            x.Title.ToLower().Contains(model.SearchTerm.ToLower())
                            )
                    .OrderBy(x => x.Title)
                    .ToList();
            }
            else
            {
                model.FoundPosts = new List<PostViewModel>();
            }

            return this.View(model);
        }


        [HttpGet]
        public ActionResult TempExample()
        {
            return this.View();
        }


        [HttpGet]
        public ActionResult SessionExample()
        {
            if (this.Session["date"] == null)
            {
                this.Session["date"] = DateTime.UtcNow;
            }
            this.Session["Pencho"] = "Pencho ot sesiqta";

            return this.View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            this.TempData["source"] = "Index";

            //var posts = this.cachingProvider
            //        .Get("cachedPosts", () =>
            //            this.postsService
            //                .GetAllMappedTo<PostViewModel>(), 60 * 1);
            var posts = this.postsService
                            .GetAllMappedTo<PostViewModel>();


            var model = new HomeViewModel()
            {
                Posts = posts
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PostViewModel model)
        {
            var post = this.mapper.Map<Post>(model);

            this.postsService.Update(post);

            // sled kato prikluchim s update.a 
            // redirektvame kam Index Gettera
            // tova se sluchva taka ili inache sega!


            return this.RedirectToAction("Index");
        }

        // API Method for Ajax

        public ActionResult GetPosts()
        {
            var posts = this.postsService
                .GetAllMappedTo<PostViewModel>()
                .OrderBy(x => x.Title)
                .ToList();

            return this.PartialView("_PostListPartial", posts);
        }

        public ActionResult GetPostsJson()
        {
            var posts = this.postsService
                .GetAllMappedTo<PostViewModel>()
                .Select(x => new { Title = x.Title, Author = x.AuthorEmail })
                .OrderBy(x => x.Title)
                .ToList();

            return this.Json(posts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}