using ProductOrderAPI.DTO.ProductDTO;
using System.Collections.Generic;

namespace ProductOrderAPI.Interfaces
{
    public interface IProductService
    {
        void NewProduct(ProductDTO product, string sellerID);
        List<ProductDTO> ProductsBySeller(string sellerID);
        List<ProductDTO> GetProducts();
        bool UpdateProduct(ProductDTO product, string sellerID, out string message);
        bool DeleteProduct(int productID, string sellerID, out string message);
        ProductDTO GetProduct(int id);
    }
}
