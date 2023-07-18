using System;
using UserAdminAPI.Common;

namespace UserAdminAPI.DTO
{
    public class UserInfoDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public ESellerStatus? Approved { get; set; }
        public string Image { get; set; }
    }
}
