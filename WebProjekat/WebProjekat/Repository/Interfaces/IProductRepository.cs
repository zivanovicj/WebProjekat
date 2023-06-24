using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetProducts();
    }
}
