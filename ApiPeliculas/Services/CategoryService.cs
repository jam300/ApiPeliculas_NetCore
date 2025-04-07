using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Exceptions;
using ApiPeliculas.Repositories.IRepository;
using ApiPeliculas.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Services
{
    public class CategoryService : ServiceBase<Category, CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
            : base(categoryRepo, mapper)
        {
          _categoryRepo = categoryRepo;
        }
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepo.CategoryExistsByNameAsync(createCategoryDto.Name))
                throw new BadRequestException($"Category '{createCategoryDto.Name}' already exists.", 400);

            var category = _mapper.Map<Category>(createCategoryDto);
            category.CreatedAt = DateTime.Now;
            return await base.CreateAsync(category);
        }
        public async Task<bool> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryDto.Id);
            if (category == null)
                throw new NotFoundException($"Category with ID {categoryDto.Id} not found.", 404);

            if (await _categoryRepo.CategoryExistsByNameAsync(categoryDto.Name) && category.Name != categoryDto.Name)
            {
                throw new BadRequestException($"The Name '{categoryDto.Name}' is already used it.");
            }

            _mapper.Map(categoryDto, category);
            category.CreatedAt = DateTime.Now;

            return await base.UpdateAsync(category);
        }
        public async Task<bool> PatchCategoryAsync(int id, JsonPatchDocument<CategoryDto> patchDoc)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found.", 404);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            patchDoc.ApplyTo(categoryDto);

            _mapper.Map(categoryDto, category);

            return await base.UpdateAsync(category);
        }
    }
}
