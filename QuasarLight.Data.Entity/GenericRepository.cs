using System.Data.Entity;
using System.Linq;
using QuasarLight.Common.Contracts.Repositories;

namespace QuasarLight.Data.Entity
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;

        protected GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T Create(T entity)
        {
            return Context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
                entry = Context.Entry(entity);
            }

            entry.State = EntityState.Modified;

            return entity;
        }

        public T Delete(T entity)
        {
            return Context.Set<T>().Remove(entity);
        }
    }
}