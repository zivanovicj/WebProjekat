using System.Collections.Generic;
using UserAdminAPI.Common;
using UserAdminAPI.DTO;

namespace UserAdminAPI.Interfaces
{
    public interface IAdminService
    {
        List<UserInfoDTO> GetCustomers();
        List<UserInfoDTO> GetSellers();
        bool SetSellerStatus(string email, ESellerStatus status);
    }
}
