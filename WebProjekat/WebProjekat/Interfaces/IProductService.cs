using System.Collections.Generic;
using WebProjekat.DTO.ProductDTO;

namespace WebProjekat.Interfaces
{
    public interface IProductService
    {
        void NewProduct(ProductDTO product, string sellerID);
        List<ProductDTO> ProductsBySeller(string sellerID);
        List<ProductDTO> GetProducts();
    }
}
