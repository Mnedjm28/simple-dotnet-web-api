using Microsoft.AspNetCore.Authorization;
using SimpleDotNetWebApiApp.Authorization;
using SimpleDotNetWebApiApp.Shared;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            // Policy Based Authorization
            services.AddSingleton<IAuthorizationHandler, AgeAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminsOnly", policy =>
                {
                    policy.RequireRole(Constants.ADMIN);
                });

                options.AddPolicy("UsersOnly", policy =>
                {
                    policy.RequireClaim("Role", Constants.USER);
                });

                options.AddPolicy("AgeGreaterThan25", policy => policy.AddRequirements(new AgeGreaterThan25Requirement()));

                options.AddPolicy("GuestsOnly", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.IsInRole(Constants.GUEST);
                    });
                });
            });

            return services;
        }
    }
}
