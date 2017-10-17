using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        IEnumerable<TDest> GetFilteredByAndMappedTo<TDest>(Expression<Func<Post, bool>> filter)
            where TDest : IMapFrom<Post>;

        IEnumerable<TDest> GetPostsByUserNameMappedTo<TDest>(string userName)
             where TDest : IMapFrom<Post>;

        IEnumerable<TDest> GetPostsFilteredForTitleOrContentMappedTo<TDest>(string searchTerm)
             where TDest : IMapFrom<Post>;

        Post CreatePost(string title, string content, string userId);

        Post Edit(object postId, string newTitle, string newContent, bool isDeleted);

        void HardDelete(object id);
    }
}