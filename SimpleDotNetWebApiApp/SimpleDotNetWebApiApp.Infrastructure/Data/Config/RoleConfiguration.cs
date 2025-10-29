using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(e => e.Users)
                   .WithOne(e => e.Role)
                   .HasForeignKey(e => e.RoleId)
                   .IsRequired();

            //Seeder
            builder.HasData(
                new Role { Id = (int)RoleEnum.Admin, Name = "Admin" },
                new Role { Id = (int)RoleEnum.User, Name = "User" },
                new Role { Id = (int)RoleEnum.Guest, Name = "Guest" }
                );
        }
    }
}
