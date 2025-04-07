using ApiPeliculas.Shared.Enums;

namespace ApiPeliculas.Entities.DTOs
{
    public class ReadUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
