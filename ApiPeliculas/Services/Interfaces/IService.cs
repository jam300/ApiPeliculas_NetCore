using System.Linq.Expressions;

namespace ApiPeliculas.Services.Interfaces
{
    public interface IService<TEntity, TDto> where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
