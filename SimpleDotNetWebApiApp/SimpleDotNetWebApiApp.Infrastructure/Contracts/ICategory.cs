using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface ICategory
    {
        public Task<List<Category>> GetItems();
        public Task<Category?> GetItem(int id);
        public Task<Category> CreateItem(Category item);
        public Task<Category> UpdateItem(Category item);
        public Task DeleteItem(int id);
    }
}
