using ApiPeliculas.Entities;
using ApiPeliculas.Repositories.Interfaces;

namespace ApiPeliculas.Repositories.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> CategoryExistsByNameAsync(string name);

    }
}
