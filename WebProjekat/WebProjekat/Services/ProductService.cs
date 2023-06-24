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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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
    }
}
