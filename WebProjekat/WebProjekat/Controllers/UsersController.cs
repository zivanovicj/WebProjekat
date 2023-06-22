using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] LogInUserDTO user)
        {
            TokenDTO token = _userService.LogInUser(user);
            if (token == null)
                return BadRequest("User doesn't exist");
            else if (String.IsNullOrEmpty(token.Token))
                return BadRequest("Incorrect password");

            return Ok(token);
        }

        [HttpPost("update")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            bool result = _userService.UpdateUser(user);
            if (result)
                return Ok();
            return BadRequest("Invalid email");
        }

        [HttpPost("passwordChange")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO data)
        {
            bool result = _userService.ChangePassword(data, out string message);
            if (result)
                return Ok();
            return BadRequest(message);
        }

    }
}
