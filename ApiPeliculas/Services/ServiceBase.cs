using ApiPeliculas.Entities;
using ApiPeliculas.Entities.Dtos;
using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Exceptions;
using ApiPeliculas.Repositories.Interfaces;
using ApiPeliculas.Repositories.IRepository;
using ApiPeliculas.Services.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace ApiPeliculas.Services
{
    public class ServiceBase<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public ServiceBase(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entities = await _repository.GetByIdAsync(id);
            if (entities == null)
                throw new NotFoundException($"Category with ID {id} not found.", 404);

            return _mapper.Map<TDto>(entities);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _repository.UpdateAsync(entity);
            return await SaveData("Update");
        }

        public async Task<TDto> CreateAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            var success = await SaveData("Create");

            if (!success)
                throw new BadRequestException("Failed to save the entity.");

            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entities = await _repository.GetByIdAsync(id);
            if (entities == null)
                throw new NotFoundException($"Category with ID {id} not found.", 404);

            _repository.Delete(entities);
   
            return await SaveData("Delete");
        }

        private async Task<bool> SaveData(string operationName)
        {
            bool succes = await _repository.SaveAsync();
            if (!succes)
                throw new BadRequestException($"Error during operation.");
            return succes;
        }
    }
}
