using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Infrastructure.Data;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyDatabaseMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GeneralAppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
