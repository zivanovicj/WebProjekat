using System.Collections.Generic;
using System.Linq;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContextWP _dbContext;
        public ProductRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
    }
}
