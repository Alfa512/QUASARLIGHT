using System.Linq;

namespace QuasarLight.Common.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}