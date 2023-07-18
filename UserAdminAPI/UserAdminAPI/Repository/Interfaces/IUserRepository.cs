using System.Collections.Generic;
using UserAdminAPI.Models;

namespace UserAdminAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(string email);
        User GetUserDetails(string email, out UserImage image);
        void AddUser(User user);
        void UpdateUser(User user);
        void AddUserImage(UserImage image);
        UserImage GetUserImage(string userID);
        void UpdateUserImage(UserImage image);
    }
}
