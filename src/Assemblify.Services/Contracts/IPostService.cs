using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Assemblify.Services.Contracts
{
    public interface IPostService
    {
        Post GetPostById(object id);
        Post GetPostByIdAndDeleted(object id);

        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllAndDeleted();

        IEnumerable<TDest> GetAllMappedTo<TDest>()
            where TDest : IMapFrom<Post>;
        IEnumerable<TDest> GetAllAndDeletedMappedTo<TDest>()
            where TDest : IMapFrom<Post>;

        IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName)
             where TDest : IMapFrom<Post>;

        Post CreatePost(string title, string content, string userId);

        void Edit(object postId, string newTitle, string newContent, bool isDeleted);

        void Update(Post post);

        void HardDelete(object id);
    }
}