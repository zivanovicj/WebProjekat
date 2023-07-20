using ProductOrderAPI.Common;
using ProductOrderAPI.Models;
using System.Collections.Generic;

namespace ProductOrderAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        bool NewOrder(Order order, List<Product> products);
        Order GetOrderByID(int orderID);
        List<OrderItem> GetOrderItems(int orderID);
        bool CancelOrder(Order order, List<Product> products);
        List<Order> GetByStatus(string customerID, EOrderStatus orderStatus);
        List<Order> GetOrders();
        List<int> GetOrderItemsByProductIDs(List<int> productIDs);
        List<Order> GetOrdersBySeller(List<int> orderIDs, EOrderStatus orderStatus);
        List<OrderItem> GetOrderItemsBySeller(int orderID, List<int> productsIDs);
    }
}
