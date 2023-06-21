using GbAviationTicketApi.Models;
using System.Linq.Expressions;

namespace GbAviationTicketApi.Repository.IRepository
{
    public interface IRepositoryBase<T> where T : ModelBase
    {
        Task<IQueryable<T>> FindAllAsync();
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> SimpleUpdateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task SaveAsync();
    }
}
