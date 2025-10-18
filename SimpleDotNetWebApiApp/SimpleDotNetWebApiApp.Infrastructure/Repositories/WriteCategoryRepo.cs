using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class WriteCategoryRepo(WriteAppDbContext dbContext) : IWriteCategoryRepo
    {
        public async Task<Category> CreateCategory(Category category)
        {
            await dbContext.Set<Category>().AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var record = await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == category.Id) ?? throw new NotFoundException("Category", category.Id);

            record.Name = category.Name;
            record.Note = category.Note;

            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException("Category", id);

            if (category == null) return;

            dbContext.Set<Category>().Remove(category);

            await dbContext.SaveChangesAsync();
        }
    }
}
