using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using AutoMapper;

namespace ApiPeliculas.PeliculasMapper
{
    public class CategoryMapper: Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
