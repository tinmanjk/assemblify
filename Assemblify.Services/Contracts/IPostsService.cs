using Assemblify.Data.Models;
using System.Linq;

namespace Assemblify.Services.Contracts
{
    public interface IPostsService
    {
        IQueryable<Post> GetAll();
        void Update(Post post);
    }
}