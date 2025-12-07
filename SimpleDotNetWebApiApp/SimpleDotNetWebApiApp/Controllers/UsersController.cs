using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleDotNetWebApiApp.Application.Dtos.User;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleDotNetWebApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserRepo userRepo, IMapper mapper, JwtOptions jwtOptions) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var existingUser = await userRepo.FindByUsernameOrEmail(request.Username, request.Email);
            if (existingUser != null)
            {
                if (request.Username == existingUser.Username)
                    return Unauthorized("This username has been used by someone else.");

                if (request.Email == existingUser.Email)
                    return Unauthorized("This email has been used by someone else.");
            }

            var user = await userRepo.Register(mapper.Map<User>(request));

            return Ok(GenerateToken(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginDto cardinalities)
        {
            var user = await userRepo.Login(mapper.Map<User>(cardinalities));

            if (user == null)
                return Unauthorized("Username or password is wrong.");

            return Ok(GenerateToken(user));
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
