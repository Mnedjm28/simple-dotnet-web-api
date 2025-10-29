using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Infrastructure.Data;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration config)
        {
            // Configure DataBase + How to call connection string configuration from appsettings.json
            services.AddDbContext<GeneralAppDbContext>(cng => cng.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ReadAppDbContext>(cng => cng.UseSqlServer(config.GetConnectionString("ReadConnection")));
            services.AddDbContext<WriteAppDbContext>(cng => cng.UseSqlServer(config.GetConnectionString("WriteConnection")));

            return services;
        }
    }
}
