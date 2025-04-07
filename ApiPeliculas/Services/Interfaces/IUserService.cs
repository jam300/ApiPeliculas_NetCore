using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserByIdAsync(string id);
        Task<ReadUserDto> CreateUserAsync(RegisterUserDto registerUserDto);
        Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserDto);
    }
}
