using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleDotNetWebApiApp.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleDotNetWebApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(JwtOptions jwtOptions, ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public ActionResult<string> AuthenticateUser(AuthenticationRequestModel request)
        {
            var user = dbContext.Set<User>().FirstOrDefault(o => o.Username == request.Username && o.Password == request.Password);

            if (user == null)
                return Unauthorized("Username or Password is wrong");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                    new Claim("DateOfBirth", "1980/10/2"),
                }),
                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return Ok(accessToken);
        }
    }
}
