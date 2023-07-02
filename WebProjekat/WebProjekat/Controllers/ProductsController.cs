using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("{email}")]
        public IActionResult GetProductsBySeller(string email)
        {
            if (!User.Identity.Name.Equals(email))
                return BadRequest("You can only view your products");

            return Ok(_productService.ProductsBySeller(email));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [AllowAnonymous]
        [HttpGet("details/{productID}")]
        public IActionResult GetProduct(string productID)
        {
            var result = Int32.TryParse(productID, out int id);
            if (!result)
                return BadRequest("Inavlid product ID");

            var product = _productService.GetProduct(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost("modify/{productID}")]
        public IActionResult ModifyProduct([FromBody] ProductDTO product, string productID)
        {
            if (!Int32.TryParse(productID, out int id))
                return BadRequest("Invalid productID");

            product.ProductID = id;
            var res = _productService.UpdateProduct(product, User.Identity.Name, out string message);

            if (!res)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete("remove/{productID}")]
        public IActionResult RemoveProduct(string productID)
        {
            if (!Int32.TryParse(productID, out int id))
                return BadRequest("Invalid productID");

            var res = _productService.DeleteProduct(id, User.Identity.Name, out string message);
            if (!res)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
