using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using WebProjekat.Common;
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
            return _dbContext.OrderItems.Where(x => x.OrderID == orderID).ToList();
        }

        public List<int> GetOrderItemsByProductIDs(List<int> productIDs)
        {
            return _dbContext.OrderItems.Where(x => productIDs.Contains(x.ProductID))
                                        .Select(x => x.OrderID)
                                        .ToList();
        }

        public List<OrderItem> GetOrderItemsBySeller(int orderID, List<int> productIDs)
        {
            return _dbContext.OrderItems.Where(x => (x.OrderID == orderID) &&
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
