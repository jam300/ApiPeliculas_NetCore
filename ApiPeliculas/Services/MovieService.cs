using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Exceptions;
using ApiPeliculas.Repositories.Interfaces;
using ApiPeliculas.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Services
{
    public class MovieService : ServiceBase<Movie, ReadMovieDto>, IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo, IMapper mapper)
            :base(movieRepo, mapper)
        {
            _movieRepo = movieRepo;
        }
        public async Task<ReadMovieDto> CreateMovieAsync(CreateMovieDto newMovie)
        {
            if (await _movieRepo.MovieExistsByNameAsync(newMovie.Name))
                throw new BadRequestException($"Movie {newMovie.Name} already exists.", 400);

            var movie = _mapper.Map<Movie>(newMovie);
            return await base.CreateAsync(movie);
        }

        public async Task<IEnumerable<ReadMovieDto>> GetAllMoviesOnCategoryAsync(int categoryId)
        {
            var allmovies = await  _movieRepo.GetAllMoviesOnCategoryAsync(categoryId);

            if(allmovies == null || !allmovies.Any())
                throw new NotFoundException($"Movies with category ID {categoryId} not found.", 404);

            return _mapper.Map<IEnumerable<ReadMovieDto>>(allmovies);
        }

        public async Task<bool> PatchMovieAsync(int id, JsonPatchDocument<ReadMovieDto> patchDoc)
        {
            var movie = await _movieRepo.GetByIdAsync(id);

            if (movie == null)
                throw new NotFoundException($"Movie with ID {id} not found.", 404);

            var movieDto = _mapper.Map<ReadMovieDto>(movie);
            patchDoc.ApplyTo(movieDto);

            _mapper.Map(movieDto, movie);

            return await base.UpdateAsync(movie);
        }

        public async Task<IEnumerable<ReadMovieDto>> SearchMovieAsync(string keyword)
        {
            var result = await _movieRepo.SearchMovieAsync(keyword);

            if (result == null || !result.Any())
                throw new NotFoundException($"Movies with {keyword} not found.", 404);

            return _mapper.Map<IEnumerable<ReadMovieDto>>(result);
        }

        public async Task<bool> UpdateMovieAsync(ReadMovieDto updateMovie)
        {
            var movie = await _movieRepo.GetByIdAsync(updateMovie.Id);

            if (movie == null)
                throw new NotFoundException($"Movie with ID {updateMovie.Id} not found.", 404);

            if (await _movieRepo.MovieExistsByNameAsync(updateMovie.Name) && movie.Name != updateMovie.Name)
            {
                throw new BadRequestException($"The Name '{updateMovie.Name}' is already used it.");
            }

            _mapper.Map(updateMovie, movie);
            movie.CreatedAt = DateTime.Now;

            return await base.UpdateAsync(movie);
        }
    }
}
