using ApiPeliculas.Entities;

namespace ApiPeliculas.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAllMoviesOnCategoryAsync(int categoryId);
        Task<IEnumerable<Movie>> SearchMovieAsync(string keyword);
        Task<bool> MovieExistsByNameAsync(string name);

    }
}
