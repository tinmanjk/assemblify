using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Services
{
    public class PostsService : IPostsService
    {
        private readonly IEfRepository<Post> postsRepo;
        private readonly ISaveContext context;

        public PostsService(IEfRepository<Post> postsRepo,
            ISaveContext context)
        {
            this.postsRepo = postsRepo;
            this.context = context;

        }

        public IQueryable<Post> GetAll()
        {
            return this.postsRepo.All;
        }

        public void Update(Post post)
        {
            this.postsRepo.Update(post);
            this.context.Commit();
            
        }
    }
}
