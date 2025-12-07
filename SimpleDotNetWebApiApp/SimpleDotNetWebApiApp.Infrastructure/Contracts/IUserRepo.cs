using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Contracts
{
    public interface IUserRepo
    {
        public Task<User?> FindByUsernameOrEmail(string username, string email);

        public Task<User> Register(User user);

        public Task<User> Login(User user);
    }
}
