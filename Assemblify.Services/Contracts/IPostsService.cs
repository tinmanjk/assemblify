using Assemblify.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assemblify.Services.Contracts
{
    public interface IPostsService
    {
        IQueryable<Post> GetAll();
        IEnumerable<T> GetGeneric<T>();

        void Update(Post post);
    }
}