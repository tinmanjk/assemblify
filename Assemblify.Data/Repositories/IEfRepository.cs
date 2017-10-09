using System.Linq;
using Assemblify.Data.Models.Contracts;

namespace Assemblify.Data.Repositories
{
    public interface IEfRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        T GetById(object id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}