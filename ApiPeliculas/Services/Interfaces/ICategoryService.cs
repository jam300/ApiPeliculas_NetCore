using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Services.Interfaces
{
    public interface ICategoryService : IService<Category, CategoryDto>
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<bool> UpdateCategoryAsync(CategoryDto categoryDto);
        Task<bool> PatchCategoryAsync(int id, JsonPatchDocument<CategoryDto> patchDoc);
    }
}
