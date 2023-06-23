using System.Collections.Generic;
using WebProjekat.Common;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IAdminRepository
    {
        List<User> GetCustomers();
        List<User> GetSellers();
        User GetSeller(string email);
        void SetSellerStatus(Seller seller);
    }
}
