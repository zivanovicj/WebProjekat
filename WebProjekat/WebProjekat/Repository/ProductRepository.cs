using System;
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
        private readonly object lockObject = new object();
        public ProductRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public bool DeleteProduct(Product product, ProductImage productImage)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                lock (lockObject)
                {
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();

                    if (productImage != null)
                    {
                        _dbContext.ProductImages.Remove(productImage);
                        _dbContext.SaveChanges();
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return false;
            }
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
            lock (lockObject)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
            }
        }

        public Product GetDetailedProduct(int id, out ProductImage image)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                lock (lockObject)
                {
                    var product = _dbContext.Products.Find(id);
                    image = _dbContext.ProductImages.Where(x => x.ProductID.Equals(product.ProductID)).FirstOrDefault();

                    transaction.Commit();
                    return product;
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                image = null;
                return null;
            }
        }
    }
}
