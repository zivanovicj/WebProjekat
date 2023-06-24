using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebProjekat.DTO.ProductDTO;
using WebProjekat.Interfaces;

namespace WebProjekat.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SELLER")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody] ProductDTO product)
        {
            string sellerID = User.Identity.Name;
            _productService.NewProduct(product, sellerID);
            return Ok();
        }

        [HttpGet("products/{email}")]
        public IActionResult GetProductsBySeller(string email)
        {
            if (!User.Identity.Name.Equals(email))
                return BadRequest("You can only view your products");

            return Ok(_productService.ProductsBySeller(email));
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
    }
}
