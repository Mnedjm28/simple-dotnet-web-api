using System.Linq.Expressions;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IGenericRepo<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(int id);

        public Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> predecate);
    }
}
