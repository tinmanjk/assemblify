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

        Post GetPostById(object id);
        Post GetPostByIdAndDeleted(object id);

        IEnumerable<Post> GetAllAndDeleted();

        IEnumerable<TDest> GetAllAndDeletedMappedTo<TDest>()
            where TDest : IMapFrom<Post>;


        bool PostExists();

        Post CreatePost(string title, string content, string userId);

        void HardDelete(object id);

        void Update(Post post);

        void Edit(object postId, string newTitle, string newContent, bool isDeleted);

        IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName)
         where TDest : IMapFrom<Post>;

    }
}