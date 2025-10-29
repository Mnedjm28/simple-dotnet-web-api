using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Data.Config;
using System;

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);
        }
    }
}
