using System.Collections.Generic;
using UserAdminAPI.Models;

namespace UserAdminAPI.Repository.Interfaces
{
    public interface IAdminRepository
    {
        List<User> GetCustomers();
        List<User> GetSellers();
        User GetSeller(string email);
        void SetSellerStatus(Seller seller);
    }
}
