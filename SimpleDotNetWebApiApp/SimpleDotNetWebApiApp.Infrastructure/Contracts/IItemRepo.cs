using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IItemRepo
    {
        public Task<List<Item>> GetItems(); 
        public Task<List<Item>> GetItemsByCategory(int categoryId); 
        public Task<Item?> GetItem(int id);
        public Task<Item> CreateItem(Item item);
        public Task<Item> UpdateItem(Item item, bool ignoreImage);
        public Task DeleteItem(int id);
    }
}
