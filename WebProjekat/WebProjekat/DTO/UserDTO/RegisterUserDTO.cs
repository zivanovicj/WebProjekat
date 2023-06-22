﻿using System;
using WebProjekat.Common;

namespace WebProjekat.DTO.UserDTO
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public EUserType UserType { get; set; }
    }
}
