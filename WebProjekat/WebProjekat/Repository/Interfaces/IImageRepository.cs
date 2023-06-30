using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IImageRepository
    {
        void AddProductImage(ProductImage image);
        ProductImage GetProductImage(int productID);
    }
}
