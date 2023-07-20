using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductOrderAPI.Models;

namespace ProductOrderAPI.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemID);
            builder.HasOne(x => x.Product)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x => x.ProductID)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderedProducts)
                   .HasForeignKey(x => x.OrderID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
