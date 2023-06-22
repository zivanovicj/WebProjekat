using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Interfaces;

namespace WebProjekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO newUser)
        {
            TokenDTO token = _userService.CreateUser(newUser, out string message);
            if (token != null)
                return Ok(token);
            else
                return BadRequest(message);
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult GetInfo(string email)
        {
            if (!User.Identity.Name.Equals(email))
                return BadRequest("You can only acess your information");
            UserInfoDTO userInfo = _userService.GetInfo(email);
            if (userInfo == null)
                return BadRequest("User doesn't exist");
            return Ok(userInfo);
        }

    }
}
