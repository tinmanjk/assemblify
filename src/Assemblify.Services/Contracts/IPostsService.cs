using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Assemblify.Services.Contracts
{
    public interface IPostsService
    {
        IEnumerable<Post> GetAll();

        IEnumerable<TDest> GetAllMappedTo<TDest>() 
            where TDest : IMapFrom<Post>;

        bool PostExists();

        Post CreatePost(string title, string content, string userId);

        void Update(Post post);

        IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName)
         where TDest : IMapFrom<Post>;

    }
}