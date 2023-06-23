using System.Collections.Generic;
using WebProjekat.Common;
using WebProjekat.DTO.UserDTO;

namespace WebProjekat.Interfaces
{
    public interface IAdminService
    {
        List<UserInfoDTO> GetCustomers();
        List<UserInfoDTO> GetSellers();
        bool SetSellerStatus(string email, ESellerStatus status);
    }
}
