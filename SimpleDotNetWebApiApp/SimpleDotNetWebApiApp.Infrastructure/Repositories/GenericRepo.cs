using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;
using System.Linq.Expressions;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class GenericRepo<T>(GeneralAppDbContext dbContext) : IGenericRepo<T> where T : class
    {
        public Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> predecate) => dbContext.Set<T>().Where(predecate).ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FindAsync(id) ?? throw new NotFoundException("T", id);

        public Task<List<T>> GetAllAsync() => dbContext.Set<T>().ToListAsync();

        public async Task<T> CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await dbContext.Set<T>().FindAsync(id);

            if (record == null) return;

            dbContext.Set<T>().Remove(record);

            await dbContext.SaveChangesAsync();
        }
    }
}
