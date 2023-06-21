using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductID);
            builder.Property(x => x.ProductID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Seller)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.SellerID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
