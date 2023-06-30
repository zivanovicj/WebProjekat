using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
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
