using AutoMapper;
using CodeFirst.Data.Context;
using CodeFirst.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeFirst.Data.Services
{
    public class DbServices : IDbService
    {
        private readonly CodeFirstContext _db;
        private readonly IMapper _mapper;

        public DbServices(CodeFirstContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<bool> SaveChangesAsync()
        {
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        async Task<TEntity> IDbService.AddAsync<TEntity, TDto>(TDto Dto)
        {
            var entity = _mapper.Map<TEntity>(Dto);
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        async Task<bool> IDbService.AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        {
            return await _db.Set<TEntity>().AnyAsync(expression);
        }

        bool IDbService.Delete<TReferenceEntity, TDto>(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TReferenceEntity>(dto);

                if (entity == null) { return false; }
                
                _db.Remove(entity);
                return true;
            }
            catch
            {
                throw;
            }
        }
        async Task<bool> IDbService.DeleteAsync<TEntity>(int id)
        {
            try
            {
                var entity = await GetByIdAsync<IEntity>(e => e.Id == id);
                if (entity == null) { return false; }

                _db.Remove(entity);
                return true;
            }
            catch
            {
                throw;
            }
        }

        async Task<List<TDto>> IDbService.GetAsync<TEntity, TDto>()
        {
            var enteties = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(enteties);
        }

        private async Task<IEntity?> GetByIdAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
        {
            return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
        }

        async Task<TDto> IDbService.GetByIdAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        {
            var entity =  await GetByIdAsync<TEntity>(expression);
            return _mapper.Map<TDto>(entity);
        }

        void IDbService.Update<TEntity, TDto>(int id, TDto Dto)
        {
            var entity = _mapper.Map<TEntity>(Dto);
            entity.Id = id;

            _db.Set<TEntity>().Update(entity);
        }
    }
}
