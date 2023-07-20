using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductOrderAPI.Interfaces;
using System.Data;
using System;

namespace ProductOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("productImage/{productID}")]
        [Authorize(Roles = "SELLER")]
        public IActionResult UploadImage(string productID, IFormFile file)
        {
            var result = Int32.TryParse(productID, out var pid);
            if (!result)
                return BadRequest("Invalid product ID");

            result = _imageService.AddProductImage(pid, User.Identity.Name, file);
            if (!result)
                return NotFound("You can only add an image to your product");
            return Ok();
        }

        [HttpGet("pimg/{productID}")]
        public IActionResult GetProductImage(string productID)
        {
            var result = Int32.TryParse(productID, out var pid);
            if (!result)
                return BadRequest("Invalid imageID");

            var image = _imageService.GetProductImage(pid);
            if (image == null)
                return NotFound();

            string imageBase64Data = Convert.ToBase64String(image.ImageData);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            return Ok(imageDataURL);
        }
        [HttpPost("pimgUpdate/{productID}")]
        [Authorize(Roles = "SELLER")]
        public IActionResult UpdateImage(string productID, IFormFile file)
        {
            var result = Int32.TryParse(productID, out var pid);
            if (!result)
                return BadRequest("Invalid product ID");

            result = _imageService.UpdateProductImage(file, User.Identity.Name, pid);
            if (!result)
                return NotFound("You can only update images of your products");
            return Ok();
        }
    }
}
