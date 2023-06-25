using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextWP _dbContext;
        public OrderRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }
        public bool NewOrder(Order order, List<Product> products)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Products.UpdateRange(products);
                _dbContext.SaveChanges();

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
