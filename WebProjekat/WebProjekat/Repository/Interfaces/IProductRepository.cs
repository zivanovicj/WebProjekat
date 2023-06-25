using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetProducts();
        Product GetProduct(int? id);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
