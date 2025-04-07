using ApiPeliculas.Entities;
using System.Linq.Expressions;

namespace ApiPeliculas.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        Task<bool> SaveAsync();
        void Delete(TEntity entity);
    }
}
