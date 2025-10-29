using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data.Config
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasOne(e => e.User)
                   .WithMany(e => e.UserPermissions)
                   .HasForeignKey(e => e.UserId)
                   .IsRequired();

            builder.HasKey(o => new { o.UserId, o.PermissionId });
        }
    }
}
