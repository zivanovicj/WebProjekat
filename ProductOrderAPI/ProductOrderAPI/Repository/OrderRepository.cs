using ProductOrderAPI.Common;
using ProductOrderAPI.Infrastructure;
using ProductOrderAPI.Models;
using ProductOrderAPI.Repository.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ProductOrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextWP _dbContext;
        private readonly object lockObject = new object();
        public OrderRepository(DbContextWP dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CancelOrder(Order order, List<Product> products)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                lock (lockObject)
                {
                    _dbContext.Products.UpdateRange(products);
                    _dbContext.SaveChanges();

                    _dbContext.Orders.Update(order);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public List<Order> GetByStatus(string customerID, EOrderStatus orderStatus)
        {
            return _dbContext.Orders.Where(x => (x.CustomerID.Equals(customerID)) &&
                                                (x.OrderStatus == orderStatus))
                                     .ToList();
        }

        public Order GetOrderByID(int orderID)
        {
            return _dbContext.Orders.Find(orderID);
        }

        public List<OrderItem> GetOrderItems(int orderID)
        {
            return _dbContext.OrderItem.Where(x => x.OrderID == orderID).ToList();
        }

        public List<int> GetOrderItemsByProductIDs(List<int> productIDs)
        {
            return _dbContext.OrderItem.Where(x => productIDs.Contains(x.ProductID))
                                        .Select(x => x.OrderID)
                                        .ToList();
        }

        public List<OrderItem> GetOrderItemsBySeller(int orderID, List<int> productIDs)
        {
            return _dbContext.OrderItem.Where(x => (x.OrderID == orderID) &&
                                                     (productIDs.Contains(x.ProductID)))
                                        .ToList();
        }

        public List<Order> GetOrders()
        {
            return _dbContext.Orders.ToList();
        }

        public List<Order> GetOrdersBySeller(List<int> orderIDs, EOrderStatus orderStatus)
        {
            return _dbContext.Orders.Where(x => (orderIDs.Contains(x.OrderID)) &&
                                                (x.OrderStatus == orderStatus))
                                    .ToList();
        }

        public bool NewOrder(Order order, List<Product> products)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                lock (lockObject)
                {
                    _dbContext.Products.UpdateRange(products);
                    _dbContext.SaveChanges();

                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
