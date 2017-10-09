using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
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
        private readonly IPostsService postsService;
        private readonly IMapper mapper;
        //private readonly ISaveContext context;

        public HomeController(IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetFiltered(int pageSize)
        {
            var posts = this.postsService
                .GetAll()
                .ProjectTo<PostViewModel>()
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
            model.FoundPosts = this.postsService
                .GetAll()
                .ProjectTo<PostViewModel>()
                .Where(x =>
                        x.Title.ToLower().Contains(model.SearchTerm1.ToLower()) ||
                        x.Title.ToLower().Contains(model.SearchTerm2.ToLower()))
                .OrderBy(x => x.Title)
                .ToList();

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
                this.Session["date"] = DateTime.Now;
            }
            this.Session["Pencho"] = "Pencho ot sesiqta";

            return this.View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            this.TempData["source"] = "Index";


            var posts = this.postsService
                .GetAll()
                .ProjectTo<PostViewModel>()
                //.MapTo<PostViewModel>() // 1. Automapper syntax sugar      
                .OrderBy(x => x.Title)
                //.ToList()
                //.Select(x => this.mapper.Map<PostViewModel>(x)) // 2. mapper.Map method needs to be done on IEnumerable
                .ToList();

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
                .GetAll()
                .ProjectTo<PostViewModel>()
                .OrderBy(x => x.Title)
                .ToList();

            return this.PartialView("_PostListPartial", posts);
        }

        public ActionResult GetPostsJson()
        {
            var posts = this.postsService
                .GetAll()
                .ProjectTo<PostViewModel>()
                .Select(x => new { Title = x.Title, Author = x.AuthorEmail })
                .OrderBy(x => x)
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