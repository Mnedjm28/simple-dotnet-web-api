using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Data;

namespace SimpleDotNetWebApiApp.Infrastructure.Repositories
{
    public class UserRepo(GeneralAppDbContext dbContext) : IUserRepo
    {
        public async Task<User?> FindByUsernameOrEmail(string username, string email) => await dbContext.Users.FirstOrDefaultAsync(o => o.Username == username || o.Email == email);

        public async Task<User> Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(User loginUser)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(o => o.Username == loginUser.Username);

            if (user == null)
                return null;

            bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password);

            if (!isPasswordMatch)
                return null;

            return user;
        }
    }
}
