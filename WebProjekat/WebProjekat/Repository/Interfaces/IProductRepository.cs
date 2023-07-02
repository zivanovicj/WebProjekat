using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetProducts();
        Product GetProduct(int? id);
        Product GetDetailedProduct(int id, out ProductImage image);
        void UpdateProduct(Product product);
        bool DeleteProduct(Product product, ProductImage productImage);
        List<int> GetProductsBySeller(string sellerID);
    }
}
