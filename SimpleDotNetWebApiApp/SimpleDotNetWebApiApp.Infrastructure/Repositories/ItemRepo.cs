using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class ItemRepo(AppDbContext dbContext) : IItemRepo
    {
        public Task<List<Item>> GetItems() => dbContext.Set<Item>().ToListAsync();

        public Task<List<Item>> GetItemsByCategory(int categoryId) => dbContext.Set<Item>().Where(o => o.CategoryId == categoryId).ToListAsync();

        public async Task<Item> GetItem(int id) =>  await dbContext.Set<Item>().FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException("Item", id);

        public async Task<Item> CreateItem(Item item)
        {
            await dbContext.Set<Item>().AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Item> UpdateItem(Item item)
        {
            var record = await GetItem(item.Id);         

            record.Name = item.Name;
            record.Price = item.Price;
            record.CategoryId = item.CategoryId;
            record.Note = item.Note;


            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetItem(id);

            if (item == null) return;

            dbContext.Set<Item>().Remove(item);

            await dbContext.SaveChangesAsync();
        }
    }
}
