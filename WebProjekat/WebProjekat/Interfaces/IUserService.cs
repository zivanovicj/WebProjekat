using Microsoft.AspNetCore.Http;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Models;

namespace WebProjekat.Interfaces
{
    public interface IUserService
    {
        TokenDTO LogInUser(LogInUserDTO user);
        TokenDTO GoogleLogInUser(GoogleLogInDTO newUser);
        TokenDTO CreateUser(RegisterUserDTO newUser, out string message);
        UserInfoDTO GetInfo(string email);
        bool UpdateUser(UpdateUserDTO user);
        bool ChangePassword(ChangePasswordDTO data, out string message);
        void AddUserImage(string userID, IFormFile file);
        UserImage GetUserImage(string userID);
        bool UpdateUserImage(IFormFile file, string userID);
    }
}
