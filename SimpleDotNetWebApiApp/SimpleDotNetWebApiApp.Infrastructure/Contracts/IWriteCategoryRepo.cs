using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IWriteCategoryRepo
    {
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
        public Task DeleteCategory(int id);
    }
}
