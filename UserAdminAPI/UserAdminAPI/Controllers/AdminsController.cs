using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserAdminAPI.Common;
using UserAdminAPI.Interfaces;

namespace UserAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            return Ok(_adminService.GetCustomers());
        }

        [HttpGet("sellers")]
        public IActionResult GetSellers()
        {
            return Ok(_adminService.GetSellers());
        }

        [HttpPost("verify/{email}")]
        public IActionResult Verify(string email)
        {
            bool result = _adminService.SetSellerStatus(email, ESellerStatus.VERIFIED);
            if (result)
                return Ok();
            return BadRequest("Seller doesn't exist or was previously rejected");
        }

        [HttpPost("reject/{email}")]
        public IActionResult Reject(string email)
        {
            bool result = _adminService.SetSellerStatus(email, ESellerStatus.REJECTED);
            if (result)
                return Ok();
            return BadRequest("Seller doesn't exist or was previously verified");
        }
    }
}
