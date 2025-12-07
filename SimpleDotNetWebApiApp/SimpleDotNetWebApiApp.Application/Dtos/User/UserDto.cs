using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Application.Dtos.User
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public RoleEnum Role { get; set; }
    }
}
