 using ApiPeliculas.Data;
using ApiPeliculas.Entities;
using ApiPeliculas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiPeliculas.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entityType = typeof(TEntity);
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (entityType == typeof(Movie))
            {
                query = query.Include("Category");
            }

            if (entityType.GetProperty("Name") != null)
            {
                return await query.OrderBy(e => EF.Property<string>(e, "Name")).ToListAsync();
            }
            else if (entityType.GetProperty("UserName") != null)
            {
                return await query.OrderBy(e => EF.Property<string>(e, "UserName")).ToListAsync();
            }
            else if (entityType.GetProperty("Id") != null)
            {
                return await query.OrderBy(e => EF.Property<int>(e, "Id")).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }

        public void UpdateAsync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
