using ProductOrderAPI.Models;

namespace ProductOrderAPI.Repository.Interfaces
{
    public interface IImageRepository
    {
        void AddProductImage(ProductImage image);
        ProductImage GetProductImage(int productID);
        void UpdateProductImage(ProductImage image);
    }
}
