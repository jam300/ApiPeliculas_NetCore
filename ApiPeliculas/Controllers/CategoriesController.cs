using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Repositories.IRepository;
using ApiPeliculas.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiPeliculas.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [Authorize(Roles = "Admin, User, Guest")]
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all categories", OperationId = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin, User, Guest")]
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [SwaggerOperation(Summary = "Retrieve a category by its ID", OperationId = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        [Authorize(Roles = "Admin, User, Guest")]
        [SwaggerOperation(Summary = "Create a new category entry", OperationId = "CreateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
                return BadRequest("The object CategoryDto cannot be null.");

            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category);

        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [SwaggerOperation(Summary = "Update a category record", OperationId = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
                return BadRequest("Invalid category data.");

            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();

        }

        [Authorize(Roles = "Admin, User")]
        [HttpPatch("{id:int}", Name = "UpdatePatchCategory")]
        [SwaggerOperation(Summary = "Update just some fields of a category record", OperationId = "UpdatePatchCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePatchCategory(int id, [FromBody] JsonPatchDocument<CategoryDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            await _categoryService.PatchCategoryAsync(id, patchDoc);
            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [SwaggerOperation(Summary = "Delete a category record", OperationId = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
