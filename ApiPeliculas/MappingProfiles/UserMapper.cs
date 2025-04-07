using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Entities;
using AutoMapper;

namespace ApiPeliculas.MappingProfiles
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AppUser, ReadUserDto>().ReverseMap();
            CreateMap<AppUser, RegisterUserDto>().ReverseMap();
            CreateMap<AppUser, LoginUserDto>().ReverseMap();
        }
    }
}
