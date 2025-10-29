using SimpleDotNetWebApiApp.Authorization;
using SimpleDotNetWebApiApp.Filters;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class FilterExtensions
    {
        public static IServiceCollection AddFilterConfiguration(this IServiceCollection services)
        {
            // Configure Filters
            services.AddControllers(options =>
            {
                options.Filters.Add<LogActivityFilter>();
                options.Filters.Add<PermissionBasedAuthorizationFilter>();
            });

            return services;
        }
    }
}
