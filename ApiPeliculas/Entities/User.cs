using ApiPeliculas.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }

    }
}
