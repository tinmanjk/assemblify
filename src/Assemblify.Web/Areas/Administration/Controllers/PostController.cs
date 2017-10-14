using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assemblify.Data;
using Assemblify.Data.Models;
using Assemblify.Services.Contracts;
using Assemblify.Web.Areas.Administration.ViewModels.Post;
using Assemblify.Infrastructure.Factories;
using Assemblify.Web.Providers.Contracts;
using AutoMapper;

namespace Assemblify.Web.Areas.Administration.Controllers
{
    public class PostController : Controller
    {
        //private MsSqlDbContext db = new MsSqlDbContext();

        private IPostsService postsService;
        private IAuthenticationProvider authenticationProvider;
        private IMapper mapper;
        private IUsersService usersService;

        public PostController(IPostsService postsService, 
            IAuthenticationProvider authenticationProvider,
            IMapper mapper,
            IUsersService usersService)
        {
            this.postsService = postsService;
            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        // GET: Administration/Post
        public ActionResult Index()
        {
            var posts = postsService.GetAllMappedTo<PostListViewModel>();
            return View(posts);

        }

        // GET: Administration/Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.authenticationProvider.CurrentUserId;
                this.postsService.CreatePost(model.Title, model.Content, userId);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //// GET: Administration/Post/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Administration/Post/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,Content,IsDeleted,CreatedOn,ModifiedOn,DeletedOn")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(post).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(post);
        //}

        //// GET: Administration/Post/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Administration/Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Post post = db.Posts.Find(id);
        //    db.Posts.Remove(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
