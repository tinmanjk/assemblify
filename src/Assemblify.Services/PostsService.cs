﻿using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assemblify.Infrastructure.Mapping;
using Assemblify.Infrastructure.Factories;

namespace Assemblify.Services
{
    public class PostsService : IPostsService
    {
        private readonly IEfRepository<Post> postsRepo;
        private readonly ISaveContext context;
        private readonly IUsersService usersService;
        private readonly IPostFactory postFactory;

        public PostsService(IEfRepository<Post> postsRepo,
            ISaveContext context,
            IPostFactory postFactory,
            IUsersService usersService)
        {
            this.postsRepo = postsRepo;
            this.context = context;
            this.postFactory = postFactory;
            this.usersService = usersService;
        }

        public Post GetPostById(object id)
        {
            return this.postsRepo.GetById(id);
        }

        public Post GetPostByIdAndDeleted(object id)
        {
            return this.postsRepo.GetByIdAndDeleted(id);
        }



        public IEnumerable<Post> GetAllAndDeleted()
        {
            return this.postsRepo
                .AllAndDeleted
                .ToList();
        }

        public IEnumerable<TDest> GetAllAndDeletedMappedTo<TDest>()
            where TDest : IMapFrom<Post>
        {
            return this.postsRepo
                .AllAndDeleted
                .MapTo<TDest>()
                .ToList();
        }

        public IEnumerable<Post> GetAll()
        {
            return this.postsRepo
                .All
                .ToList();
        }

        public IEnumerable<TDest> GetAllMappedTo<TDest>()
            where TDest : IMapFrom<Post>
        {
            return this.postsRepo
                    .All
                    .MapTo<TDest>()
                    .ToList();
        }

        public void Edit(object postId, string newTitle, string newContent, bool isDeleted)
        {
            var post = this.postsRepo.GetByIdAndDeleted(postId);

            if (post != null)
            {
                post.Title = newTitle;
                post.Content = newContent;
                post.IsDeleted = isDeleted;

                this.postsRepo.Update(post);
                this.context.Commit();
            }
        }

        public void Update(Post post)
        {
            this.postsRepo.Update(post);
            this.context.Commit();
        }

        public bool PostExists()
        {
            throw new NotImplementedException();
        }


        public Post CreatePost(string title, string content, string userId)
        {
            var user = this.usersService.GetUserById(userId);
            var post = this.postFactory.CreatePost(title, content, user);

            this.postsRepo.Add(post);

            this.context.Commit();


            return post;
        }

        public IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName) where TDest : IMapFrom<Post>
        {

            return this.postsRepo
                    .All
                    .Where(x => x.Author.UserName == userName.ToLower())
                    .MapTo<TDest>()
                    .ToList();
        }

        public void HardDelete(object id)
        {
            var toBeHardDeleted = this.postsRepo.GetByIdAndDeleted(id);
            if (toBeHardDeleted != null)
            {
                this.postsRepo.HardDelete(toBeHardDeleted);
                this.context.Commit();
            }
        }
    }
}
