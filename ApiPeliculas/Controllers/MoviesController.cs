using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Exceptions;
using ApiPeliculas.Services;
using ApiPeliculas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiPeliculas.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize(Roles = "Admin, User, Guest")]
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all movies", OperationId = "GetAllMovies")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id:int}", Name = "GetMovieByIdAsync")]
        [SwaggerOperation(Summary = "Retrieve a movie by its ID", OperationId = "GetMovieById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var movies = await _movieService.GetByIdAsync(id);
            return Ok(movies);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("category/{categoryId:int}", Name = "GetAllMoviesOnCategoryAsync")]
        [SwaggerOperation(Summary = "Retrieve all movies by Category ID", OperationId = "GetMoviesByCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMoviesOnCategoryAsync(int categoryId)
        {
            var movies = await _movieService.GetAllMoviesOnCategoryAsync(categoryId);
            return Ok(movies);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("Search/{keyWord}", Name = "SearchMovieAsync")]
        [SwaggerOperation(Summary = "Search movies by keyword", OperationId = "SearchMovies")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMovieAsync(string keyWord)
        {
            var movies = await _movieService.SearchMovieAsync(keyWord);
            return Ok(movies);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Create a new movie record", OperationId = "CreateMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDto newMovie)
        {
            if (newMovie == null)
                return BadRequest("The object CreateMovieDto cannot be null.");

            var movie = await _movieService.CreateMovieAsync(newMovie);
            return CreatedAtRoute("GetMovieByIdAsync", new { id = movie.Id }, movie);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [SwaggerOperation(Summary = "Update a movie record", OperationId = "UpdateMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMovieAsync(int id, [FromBody] ReadMovieDto updateMovie)
        {
            if (updateMovie == null || id != updateMovie.Id)
                return BadRequest("Invalid category data.");

           var succes = await _movieService.UpdateMovieAsync(updateMovie);
           if(!succes)
                return BadRequest("Something went wrong , try later.");

            return NoContent();

        }

        [Authorize(Roles = "Admin, User")]
        [HttpPatch("{id:int}", Name = "PatchMovieAsync")]
        [SwaggerOperation(Summary = "Update just some fields of a movie record", OperationId = "PatchMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchMovieAsync(int id, [FromBody] JsonPatchDocument<ReadMovieDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

           var succes =  await _movieService.PatchMovieAsync(id, patchDoc);

            if (!succes)
                return BadRequest("Something went wrong , try later.");


            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id:int}", Name = "DeleteMovieAsync")]
        [SwaggerOperation(Summary = "Delete a movie record", OperationId = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            await _movieService.DeleteAsync(id);
            return NoContent();
        }

    }
}
