using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IReadCategoryRepo
    {
        public Task<List<Category>> GetCategories();
        public Task<Category?> GetCategory(int id);
    }
}
