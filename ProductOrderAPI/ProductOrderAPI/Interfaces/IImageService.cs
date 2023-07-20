using Microsoft.AspNetCore.Http;
using ProductOrderAPI.Models;

namespace ProductOrderAPI.Interfaces
{
    public interface IImageService
    {
        bool AddProductImage(int productID, string sellerID, IFormFile file);
        ProductImage GetProductImage(int productID);
        bool UpdateProductImage(IFormFile file, string sellerID, int productID);
    }
}
