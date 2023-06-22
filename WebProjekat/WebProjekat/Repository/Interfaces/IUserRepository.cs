using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(string email);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
