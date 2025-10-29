using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(e => e.Order)
                                       .WithMany(e => e.OrderItems)
                                       .HasForeignKey(e => e.OrderId)
                                       .IsRequired();

            builder.HasOne(e => e.Item)
                                       .WithMany(e => e.OrderItems)
                                       .HasForeignKey(e => e.ItemId)
                                       .IsRequired();

            // Columns
            builder.Property(o => o.Price)
                   .HasPrecision(18, 2);
        }
    }
}
