using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Data;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class ItemRepo : GenericRepo<Item>, IItemRepo
    {
        protected readonly GeneralAppDbContext _dbContext;

        public ItemRepo(GeneralAppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Item>> GetItemsByCategoryAsync(int categoryId) => _dbContext.Set<Item>().Where(o => o.CategoryId == categoryId).ToListAsync();

        public async Task<Item> UpdateAsync(Item item, bool ignoreImage = false)
        {
            var record = await GetByIdAsync(item.Id);

            record.Name = item.Name;
            record.Price = item.Price;
            record.CategoryId = item.CategoryId;
            record.Note = item.Note;

            if (!ignoreImage)
                record.Image = item.Image;

            await _dbContext.SaveChangesAsync();
            return item;
        }

        public override Task<Item> UpdateAsync(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
