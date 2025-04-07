using ApiPeliculas.Data;
using ApiPeliculas.Entities;
using ApiPeliculas.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPeliculas.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {
        }
        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            var category =  await FindAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return category.Any();

        }

    }
}
