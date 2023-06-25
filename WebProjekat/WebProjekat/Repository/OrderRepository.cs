using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool CancelOrder(Order order, List<Product> products)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Products.UpdateRange(products);
                _dbContext.SaveChanges();

                _dbContext.Orders.Update(order);
                _dbContext.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public Order GetOrderByID(int orderID)
        {
            return _dbContext.Orders.Find(orderID);
        }

        public List<OrderItem> GetOrderItems(int orderID)
        {
            return _dbContext.OrderItems.Where(x => x.OrderID == orderID).ToList();
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
