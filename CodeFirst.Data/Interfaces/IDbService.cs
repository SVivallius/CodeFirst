using CodeFirst.Common.DTOs;
using CodeFirst.Data.Entities;
using System.Linq.Expressions;

namespace CodeFirst.Data.Interfaces
{
    public interface IDbService
    {
        public Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class;

        public Task<TDto> GetByIdAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;

        public Task<bool> DeleteAsync<TEntity>(int id)
            where TEntity : class, IEntity;

        public bool Delete<TReferenceEntity, TDto>(TDto dto)
            where TDto : class, IEntity
            where TReferenceEntity : class, IReferenceEntity;

        public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity;

        public Task<bool> SaveChangesAsync();

        public Task<TEntity> AddAsync<TEntity, TDto>(TDto Dto)
            where TEntity : class, IEntity
            where TDto : class;

        public void Update<TEntity, TDto>(int id, TDto Dto)
            where TEntity : class, IEntity
            where TDto : class;
    }
}
