using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SimpleDotNetWebApiApp.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());

            var authHeader = Request.Headers["Authorization"].ToString();

            if (!authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));

            var encodedCerdenials = authHeader.Substring("Basic ".Length).Trim();
            var usernameAndPassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCerdenials)).Split(":");

            if (usernameAndPassword[0] != "admin" || usernameAndPassword[1] != "password")
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, usernameAndPassword[0]),
            ], "Basic");

            var principle = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principle, "Basic");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
