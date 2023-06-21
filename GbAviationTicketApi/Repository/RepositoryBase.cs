using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GbAviationTicketApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {
        protected readonly IGbavsContext _db;
        internal DbSet<T> dbSet;
        public RepositoryBase(IGbavsContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsActive = false;
            _ = await SimpleUpdateAsync(entity);
        }

        public Task<IQueryable<T>> FindAllAsync()
        {
            IQueryable<T> query = dbSet;
            return Task.FromResult(query.AsNoTracking().Where(e => e.IsActive == true));
        }

        public Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            return Task.FromResult(query.AsNoTracking().Where(e => e.IsActive == true).Where(filter));
        }

        public async Task SaveAsync() => await _db.SaveAsync();

        public async Task<T?> SimpleUpdateAsync(T entity)
        {
            entity.ModifiedAt = DateTime.UtcNow;
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
        public abstract Task<T?> UpdateAsync(T entity);
    }
}
