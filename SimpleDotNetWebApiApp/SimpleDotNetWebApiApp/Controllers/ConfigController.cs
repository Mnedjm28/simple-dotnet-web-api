using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SimpleDotNetWebApiApp.Controllers
{
    [ApiController]
    [Route("")]
    public class ConfigController : ControllerBase
    {
        readonly IConfiguration _configuration;
        readonly IOptions<AttachmentOptions> _attachmentOptions;

        public ConfigController(IConfiguration configuration, IOptions<AttachmentOptions> attachmentOptions)
        {
            _attachmentOptions = attachmentOptions;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles = $"{Constants.ADMIN}")]
        public ActionResult Get()
        {
            var configs = new
            {
                EnvName = _configuration["ASPNETCORE_ENVIRONMENT"],
                AllowedHosts = _configuration["AllowedHosts"],
                ConnectionString = _configuration.GetConnectionString("DefaultConnection"),
                DefaultLogLevel = _configuration["Logging:LogLevel:Default"],
                TestKey = _configuration["TestKey"],
                SigningKey = _configuration["SigningKey"],
                AttachmentOptions = _attachmentOptions.Value
            };

            return Ok(configs);
        }
    }
}
