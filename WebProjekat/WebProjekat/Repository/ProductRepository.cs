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

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public Product GetProduct(int? id)
        {
            return _dbContext.Products.Find(id);
        }

        public List<int> GetProductsBySeller(string sellerID)
        {
            return _dbContext.Products.Where(x => x.SellerID.Equals(sellerID))
                                      .Select(x => x.ProductID)
                                      .ToList();
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
