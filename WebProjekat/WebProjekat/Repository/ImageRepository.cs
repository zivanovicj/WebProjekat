using System.Linq;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DbContextWP _dbContext;
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
    }
}
