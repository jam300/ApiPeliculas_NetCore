using ApiPeliculas.Data;
using ApiPeliculas.Entities;
using ApiPeliculas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesOnCategoryAsync(int categoryId)
        {
            return await _dbContext.Movies
                 .Where(ca => ca.CategoryId == categoryId)
                 .Include(ca => ca.Category)
                 .ToListAsync();
        }
        public async Task<IEnumerable<Movie>> SearchMovieAsync(string keyword)
        {
            IQueryable<Movie> query = _dbContext.Movies.Include(m => m.Category);
            
            if (string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(m => EF.Functions.Like(m.Name, $"{keyword}%") ||
                                  EF.Functions.Like(m.Description, $"{keyword}%"));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> MovieExistsByNameAsync(string name)
        {
            var movie = await FindAsync(c => EF.Functions.Like(c.Name.ToLower().Trim(), name.ToLower().Trim()));
            return movie.Any();
        }
    }
}
