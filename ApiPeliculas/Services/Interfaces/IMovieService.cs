using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Services.Interfaces
{
    public interface IMovieService : IService<Movie, ReadMovieDto>
    {
        Task<IEnumerable<ReadMovieDto>> GetAllMoviesOnCategoryAsync(int categoryId);
        Task<IEnumerable<ReadMovieDto>> SearchMovieAsync(string keyword);
        Task<ReadMovieDto> CreateMovieAsync(CreateMovieDto newMovie);
        Task<bool> UpdateMovieAsync(ReadMovieDto updateMovie);
        Task<bool> PatchMovieAsync(int id, JsonPatchDocument<ReadMovieDto> patchDoc);
    }
}
