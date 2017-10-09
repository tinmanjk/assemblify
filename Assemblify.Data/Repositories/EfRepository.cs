using Assemblify.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Data.Repositories
{
    public class EfRepository<T> : IEfRepository<T> where T : class, IDataModel
    {
        private readonly MsSqlDbContext context;
        private readonly DbSet<T> dbSet;

        public EfRepository(MsSqlDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public T GetById(object id)
        {
            var item = this.dbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public IQueryable<T> All
        {
            get
            {
                return this.dbSet.Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.dbSet;
            }
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
