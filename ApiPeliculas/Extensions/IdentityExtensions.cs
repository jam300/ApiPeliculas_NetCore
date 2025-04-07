using ApiPeliculas.Data;
using ApiPeliculas.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace ApiPeliculas.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(opciones =>
                                    opciones.UseSqlServer(configuration.GetConnectionString("ConexionSql")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            });

            

            return services;
        }
    }
}
