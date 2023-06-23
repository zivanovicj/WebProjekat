using System.Collections.Generic;
using System.Linq;
using WebProjekat.Common;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DbContextWP _dbContext;
        public AdminRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetCustomers()
        {
            return _dbContext.Users.Where(x => x.UserType ==  EUserType.CUSTOMER).ToList();
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
            _dbContext.Users.Update(seller);
            _dbContext.SaveChanges();
        }
    }
}
