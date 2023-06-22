using System.Collections.Generic;
using System.Linq;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextWP _dbContext;
        public UserRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUser(string email)
        {
            return _dbContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
    }
}
