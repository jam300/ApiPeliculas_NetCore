using Microsoft.AspNetCore.Identity;

namespace ApiPeliculas.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
