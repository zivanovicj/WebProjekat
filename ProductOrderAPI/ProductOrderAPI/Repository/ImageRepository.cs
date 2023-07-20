using ProductOrderAPI.Infrastructure;
using ProductOrderAPI.Models;
using ProductOrderAPI.Repository.Interfaces;
using System.Linq;

namespace ProductOrderAPI.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DbContextWP _dbContext;
        private readonly object lockObject = new object();
        public ImageRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddProductImage(ProductImage image)
        {
            _dbContext.ProductImages.Add(image);
            _dbContext.SaveChanges();
        }

        public ProductImage GetProductImage(int productID)
        {
            return _dbContext.ProductImages.Where(x => x.ProductID == productID).FirstOrDefault();
        }

        public void UpdateProductImage(ProductImage image)
        {
            lock (lockObject)
            {
                _dbContext.ProductImages.Update(image);
                _dbContext.SaveChanges();
            }
        }
    }
}
