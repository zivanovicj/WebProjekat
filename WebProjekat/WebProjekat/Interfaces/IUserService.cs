using WebProjekat.DTO.UserDTO;

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
    }
}
