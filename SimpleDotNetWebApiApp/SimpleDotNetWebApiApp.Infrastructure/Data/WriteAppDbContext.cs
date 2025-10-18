using Microsoft.EntityFrameworkCore;

namespace SimpleDotNetWebApiApp.Infrastructure.Data
{
    public class WriteAppDbContext : AppDbContext<WriteAppDbContext>
    {
        public WriteAppDbContext(DbContextOptions<WriteAppDbContext> options) : base(options)
        {
        }
    }
}
