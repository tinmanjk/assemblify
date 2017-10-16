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
using Assemblify.Common;
using Assemblify.Web.Filters;

namespace Assemblify.Web.Areas.Administration.Controllers
{
    [AdministrationAreaAuthorizationFilter(Roles = GlobalConstants.AdministratorRoleName)]

    public class PostController : Controller
    {
        private IPostService postsService;
        private IAuthenticationProvider authenticationProvider;
        private IMapper mapper;

        public PostController(IPostService postsService,
            IAuthenticationProvider authenticationProvider,
            IMapper mapper)
        {
            this.postsService = postsService;
            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
        }

        // GET: Administration/Post
        public ActionResult Index()
        {
            var posts = postsService.GetAllAndDeletedMappedTo<PostListViewModel>();
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
                var createdPost = this.postsService.CreatePost(model.Title, model.Content, userId);
                if (createdPost != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(GlobalConstants.ErrorNotCreatedPostKey, GlobalConstants.ErrorNotCreatedPostValue);
                }
            }

            return View(model);
        }

        // GET: Administration/Post/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = postsService.GetPostByIdAndDeleted(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<PostEditViewModel>(post);

            return View(model);
        }

        //POST: Administration/Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editedPost = this.postsService.Edit(model.Id, model.Title, model.Content, model.IsDeleted);

                if (editedPost != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(GlobalConstants.ErrorNotEditedPostKey, GlobalConstants.ErrorNotEditedPostValue);

                }
   
            }

            return View(model);
        }

        //// GET: Administration/Post/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = postsService.GetPostByIdAndDeleted(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<PostDeleteViewModel>(post);

            return View(model);
        }

        //// POST: Administration/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.postsService.HardDelete(id);
            return RedirectToAction("Index");
        }
    }
}
