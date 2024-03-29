﻿using System;
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
        private readonly object lockObject = new object();
        public UserRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddUserImage(UserImage image)
        {
            _dbContext.UserImages.Add(image);
            _dbContext.SaveChanges();
        }

        public UserImage GetUserImage(string userID)
        {
            return _dbContext.UserImages.Where(x => x.UserID == userID).FirstOrDefault();
        }

        public User GetUser(string email)
        {
            return _dbContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void UpdateUserImage(UserImage image)
        {
            lock (lockObject)
            {
                _dbContext.UserImages.Update(image);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            lock (lockObject)
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
            }
        }

        public User GetUserDetails(string email, out UserImage image)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                lock (lockObject)
                {
                    var user = _dbContext.Users.Find(email);
                    image = _dbContext.UserImages.Where(x => x.UserID.Equals(email)).FirstOrDefault();

                    transaction.Commit();
                    return user;
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                image = null;
                return null;
            }
        }
    }
}
