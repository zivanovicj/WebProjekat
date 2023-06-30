using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebProjekat.DTO.ProductDTO;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public bool DeleteProduct(int productID, string sellerID, out string message)
        {
            Product product = _productRepository.GetProduct(productID);
            ProductImage productImage = _imageRepository.GetProductImage(productID);
            if (product == null)
            {
                message = "Product doesn't exist";
                return false;
            }

            if (!product.SellerID.Equals(sellerID))
            {
                message = "You can only delete your products";
                return false;
            }

            _productRepository.DeleteProduct(product, productImage);
            message = "Success";
            return true;
        }

        public List<ProductDTO> GetProducts()
        {
            return _mapper.Map<List<ProductDTO>>(_productRepository.GetProducts());
        }

        public void NewProduct(ProductDTO product, string sellerID)
        {
            Product newProduct = _mapper.Map<Product>(product);
            newProduct.SellerID = sellerID;

            _productRepository.AddProduct(newProduct);
        }

        public List<ProductDTO> ProductsBySeller(string sellerID)
        {
            var products = _productRepository.GetProducts();
            return _mapper.Map<List<ProductDTO>>(products.Where(x => x.SellerID.Equals(sellerID)));
        }

        public bool UpdateProduct(ProductDTO product, string sellerID, out string message)
        {
            if(product == null)
            {
                message = "Ivalid product ID";
                return false;
            }
            Product oldProduct = _productRepository.GetProduct(product.ProductID);
            if (oldProduct == null)
            {
                message = "Product doesn't exist";
                return false;
            }

            if (!oldProduct.SellerID.Equals(sellerID))
            {
                message = "You can only modify your products";
                return false;
            }

            oldProduct.ProductName = product.ProductName;
            oldProduct.Amount = product.Amount;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;

            _productRepository.UpdateProduct(oldProduct);

            message = "Product updated";
            return true;
        }
    }
}
