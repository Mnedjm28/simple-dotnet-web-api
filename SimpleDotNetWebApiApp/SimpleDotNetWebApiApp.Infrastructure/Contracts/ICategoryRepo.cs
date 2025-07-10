using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface ICategoryRepo
    {
        public Task<List<Category>> GetCategories();
        public Task<Category?> GetCategory(int id);
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
        public Task DeleteCategory(int id);
    }
}
