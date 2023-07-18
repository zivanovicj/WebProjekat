using System.Collections.Generic;
using System.Linq;
using UserAdminAPI.Common;
using UserAdminAPI.Infrastructure;
using UserAdminAPI.Models;
using UserAdminAPI.Repository.Interfaces;

namespace UserAdminAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DbContextWP _dbContext;
        private readonly object lockObject = new object();
        public AdminRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetCustomers()
        {
            return _dbContext.Users.Where(x => x.UserType == EUserType.CUSTOMER).ToList();
        }

        public User GetSeller(string email)
        {
            return _dbContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public List<User> GetSellers()
        {
            return _dbContext.Users.Where(x => x.UserType == EUserType.SELLER).ToList();
        }

        public void SetSellerStatus(Seller seller)
        {
            lock (lockObject)
            {
                _dbContext.Users.Update(seller);
                _dbContext.SaveChanges();
            }
        }
    }
}
