﻿using WebProjekat.DTO.UserDTO;

namespace WebProjekat.Interfaces
{
    public interface IUserService
    {
        TokenDTO LogInUser(LogInUserDTO user);
        TokenDTO CreateUser(RegisterUserDTO newUser, out string message);
        UserInfoDTO GetInfo(string email);
    }
}
