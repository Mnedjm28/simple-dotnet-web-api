using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleDotNetWebApiApp.Shared;

namespace SimpleDotNetWebApiApp.Controllers
{
    [ApiController]
    [Route("")]
    public class ConfigController(IConfiguration configuration, IOptions<AttachmentOptions> attachmentOptions) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{Constants.ADMIN}")]
        public ActionResult Get()
        {
            var configs = new
            {
                EnvName = configuration["ASPNETCORE_ENVIRONMENT"],
                AllowedHosts = configuration["AllowedHosts"],
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                DefaultLogLevel = configuration["Logging:LogLevel:Default"],
                TestKey = configuration["TestKey"],
                SigningKey = configuration["SigningKey"],
                AttachmentOptions = attachmentOptions.Value
            };

            return Ok(configs);
        }
    }
}
