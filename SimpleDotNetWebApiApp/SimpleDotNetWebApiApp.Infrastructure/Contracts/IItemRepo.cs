using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IItemRepo : IGenericRepo<Item>
    {
        public Task<List<Item>> GetItemsByCategoryAsync(int categoryId);

        public Task<Item> UpdateAsync(Item entity, bool ignoreImage = false);
    }
}
