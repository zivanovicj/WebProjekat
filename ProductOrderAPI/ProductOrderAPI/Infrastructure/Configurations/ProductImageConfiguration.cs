using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductOrderAPI.Models;

namespace ProductOrderAPI.Infrastructure.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.ProductImageID);
            builder.Property(x => x.ProductImageID).ValueGeneratedOnAdd();
        }
    }
}
