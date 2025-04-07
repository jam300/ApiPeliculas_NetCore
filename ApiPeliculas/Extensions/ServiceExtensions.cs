using ApiPeliculas.Repositories.Interfaces;
using ApiPeliculas.Repositories.IRepository;
using ApiPeliculas.Repositories;
using ApiPeliculas.Services.Interfaces;
using ApiPeliculas.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiPeliculas.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            return services;
        }

        public static IServiceCollection AddConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
