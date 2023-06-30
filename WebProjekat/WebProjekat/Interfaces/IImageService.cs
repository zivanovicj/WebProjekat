using Microsoft.AspNetCore.Http;
using WebProjekat.Models;

namespace WebProjekat.Interfaces
{
    public interface IImageService
    {
        bool AddProductImage(int productID, string sellerID, IFormFile file);
        ProductImage GetProductImage(int productID);
    }
}
