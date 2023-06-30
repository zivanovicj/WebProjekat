using Microsoft.AspNetCore.Http;
using System.IO;
using WebProjekat.Common;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;

        public ImageService(IImageRepository imageRepository, IProductRepository productRepository)
        {
            _imageRepository = imageRepository;
            _productRepository = productRepository;
        }

        public bool AddProductImage(int productID, string sellerID, IFormFile file)
        {
            var product = _productRepository.GetProduct(productID);
            if (product == null)
                return false;

            if(!product.SellerID.Equals(sellerID)) 
                return false;

            ProductImage image = new ProductImage()
            {
                ImageTitle = file.FileName,
                ProductID = productID
            };

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            image.ImageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            _imageRepository.AddProductImage(image);
            return true;
        }

        public ProductImage GetProductImage(int productID)
        {
            return _imageRepository.GetProductImage(productID);
        }

        public bool UpdateProductImage(IFormFile file, string sellerID, int productID)
        {
            var product = _productRepository.GetProduct(productID);
            if(product == null) return false;
            if(product.SellerID  != sellerID) return false;

            var image = _imageRepository.GetProductImage(productID);

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            image.ImageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            _imageRepository.UpdateProductImage(image);
            return true;
        }
    }
}
