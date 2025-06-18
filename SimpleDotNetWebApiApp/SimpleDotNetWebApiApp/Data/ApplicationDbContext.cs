using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Data.Models;

namespace SimpleDotNetWebApiApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(o => new { o.UserId, o.PermissionId });

            modelBuilder.Entity<Role>().ToTable("Roles")
                                       .HasMany(e => e.Users)
                                       .WithOne(e => e.Role)
                                       .HasForeignKey(e => e.RoleId)
                                       .IsRequired();

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = (int)RoleEnum.Admin, Name = "Admin" },
                new Role { Id = (int)RoleEnum.User, Name = "User" },
                new Role { Id = (int)RoleEnum.Guest, Name = "Guest" }
                );
        }
    }
}
