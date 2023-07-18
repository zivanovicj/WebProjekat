using System;
using UserAdminAPI.Common;

namespace UserAdminAPI.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public EUserType UserType { get; set; }
    }
}
