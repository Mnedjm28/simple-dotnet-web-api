using Microsoft.EntityFrameworkCore;

namespace SimpleDotNetWebApiApp.Infrastructure.Data
{
    public class ReadAppDbContext : AppDbContext<ReadAppDbContext>
    {
        public ReadAppDbContext(DbContextOptions<ReadAppDbContext> options) : base(options)
        {
        }
    }
}
