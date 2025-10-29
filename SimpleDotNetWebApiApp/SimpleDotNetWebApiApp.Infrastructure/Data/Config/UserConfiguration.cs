using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(e => e.Role)
                   .WithMany(e => e.Users)
                   .HasForeignKey(e => e.RoleId)
                   .IsRequired();
        }
    }
}
