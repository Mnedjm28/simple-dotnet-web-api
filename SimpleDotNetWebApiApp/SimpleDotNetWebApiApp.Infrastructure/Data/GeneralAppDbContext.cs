using Microsoft.EntityFrameworkCore;

namespace SimpleDotNetWebApiApp.Infrastructure.Data
{
    public class GeneralAppDbContext : AppDbContext<GeneralAppDbContext>
    {
        public GeneralAppDbContext(DbContextOptions<GeneralAppDbContext> options) : base(options)
        {
        }
    }
}
