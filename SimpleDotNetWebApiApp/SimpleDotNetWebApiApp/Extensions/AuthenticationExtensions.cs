using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleDotNetWebApiApp.Authentication;
using System.Text;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            /*
             * JWT Bearer Authentication Shceme
            */            
            var jwtOptions = config.GetSection("Jwt").Get<JwtOptions>();
            services.AddSingleton(jwtOptions);
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer, // for multi issuers use ValidIssuers
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience, // for multi audiences use ValidAudiences
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                };
            });

            /*
             * Basic Authentication Shceme
            */            
            //services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            return services;
        }
    }
}
