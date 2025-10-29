using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data.Config
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(e => e.Category)
                   .WithMany(e => e.Items)
                   .HasForeignKey(e => e.CategoryId)
                   .IsRequired();
        }
    }
}
