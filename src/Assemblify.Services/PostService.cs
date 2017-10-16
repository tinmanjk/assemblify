using Assemblify.Data.Models;
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
using Assemblify.Infrastructure.Providers.Contracts;

namespace Assemblify.Services
{
    public class PostService : IPostService
    {
        private readonly IEfRepository<Post> postsRepo;
        private readonly ISaveContext context;
        private readonly IUserService usersService;
        private readonly IPostFactory postFactory;
        private readonly IDateTimeProvider dateTimeProvider;

        public PostService(IEfRepository<Post> postsRepo,
            ISaveContext context,
            IPostFactory postFactory,
            IUserService usersService,
            IDateTimeProvider dateTimeProvider)
        {

            if (postsRepo == null)
            {
                throw new ArgumentNullException(nameof(postsRepo));
            }


            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (postFactory == null)
            {
                throw new ArgumentNullException(nameof(postFactory));
            }


            if (usersService == null)
            {
                throw new ArgumentNullException(nameof(usersService));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

            this.postsRepo = postsRepo;
            this.context = context;
            this.postFactory = postFactory;
            this.usersService = usersService;
            this.dateTimeProvider = dateTimeProvider;
        }

        public Post GetPostById(object id)
        {
            return this.postsRepo.GetById(id);
        }

        public Post GetPostByIdAndDeleted(object id)
        {
            return this.postsRepo.GetByIdAndDeleted(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return this.postsRepo
                .All
                .ToList();
        }

        public IEnumerable<Post> GetAllAndDeleted()
        {
            return this.postsRepo
                .AllAndDeleted
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

        public IEnumerable<TDest> GetAllAndDeletedMappedTo<TDest>()
            where TDest : IMapFrom<Post>
        {
            return this.postsRepo
                .AllAndDeleted
                .MapTo<TDest>()
                .ToList();
        }

        public IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName)
            where TDest : IMapFrom<Post>
        {

            return this.postsRepo
                    .All
                    .Where(x => x.Author.UserName.ToLower() == userName.ToLower())
                    .MapTo<TDest>()
                    .ToList();
        }

        public IEnumerable<TDest> GetPostsFilteredForTitleOrContentMappedTo<TDest>(string searchTerm)
            where TDest : IMapFrom<Post>
        {
            var searchTermtoLower = searchTerm.ToLower();

            return this.postsRepo
                    .All
                    .Where(x =>
                          x.Title.ToLower().Contains(searchTermtoLower) ||
                          x.Content.ToLower().Contains(searchTermtoLower)
                          )
                    .OrderByDescending(x => x.CreatedOn)
                    .MapTo<TDest>()
                    .ToList();
        }


        public Post Edit(object postId, string newTitle, string newContent, bool isDeleted)
        {
            var post = this.postsRepo.GetByIdAndDeleted(postId);

            if (post != null)
            {
                post.Title = newTitle;
                post.Content = newContent;
                post.IsDeleted = isDeleted;

                try
                {
                    this.postsRepo.Update(post);
                    this.context.Commit();
                    return post;
                }
                catch (Exception e)
                {

                    return null;
                }
            }

            return null;
        }


        public Post CreatePost(string title, string content, string userId)
        {
            var user = this.usersService.GetUserById(userId);
            var post = this.postFactory.CreatePost(title, content, user);

            try
            {
                this.postsRepo.Add(post);

                this.context.Commit();
            }
            catch (Exception e)
            {
                return null;
            }

            return post;
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
