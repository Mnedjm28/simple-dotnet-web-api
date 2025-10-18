using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data
{
    public class AppDbContext<T> : DbContext where T : DbContext
    {
        public AppDbContext(DbContextOptions<T> options) : base(options) { }

        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
