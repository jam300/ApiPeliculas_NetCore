using ApiPeliculas.Shared.Enums;

namespace ApiPeliculas.Entities.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ReadUserDto User { get; set; }
    }
}
