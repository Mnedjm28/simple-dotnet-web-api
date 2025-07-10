using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class CategoryRepo(AppDbContext dbContext) : ICategoryRepo
    {
        public Task<List<Category>> GetCategories() => dbContext.Set<Category>().ToListAsync();

        public async Task<Category> GetCategory(int id) => await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException("Category", id);

        public async Task<Category> CreateCategory(Category category)
        {
            await dbContext.Set<Category>().AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var record = await GetCategory(category.Id);

            record.Name = category.Name;
            record.Note = category.Note;

            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategory(id);

            if (category == null) return;

            dbContext.Set<Category>().Remove(category);

            await dbContext.SaveChangesAsync();
        }
    }
}
