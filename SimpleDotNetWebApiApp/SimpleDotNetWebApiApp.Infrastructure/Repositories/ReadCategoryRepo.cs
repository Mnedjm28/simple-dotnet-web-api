using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class ReadCategoryRepo(ReadAppDbContext dbContext) : IReadCategoryRepo
    {
        public Task<List<Category>> GetCategories() => dbContext.Set<Category>().ToListAsync();

        public async Task<Category> GetCategory(int id) => await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException("Category", id);
    }
}
