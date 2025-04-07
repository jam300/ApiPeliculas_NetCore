using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using AutoMapper;

namespace ApiPeliculas.MappingProfiles
{
    public class MovieMapper : Profile
    {
        public MovieMapper()
        {
            CreateMap<Movie, ReadMovieDto>()
                    .ForMember(dest => dest.Classification, opt => opt.MapFrom(src => src.Classification.ToString()))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
        }
    }
}
