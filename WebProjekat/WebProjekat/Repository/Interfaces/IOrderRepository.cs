using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IOrderRepository
    {
        bool NewOrder(Order order, List<Product> products);
        Order GetOrderByID(int orderID);
        List<OrderItem> GetOrderItems(int orderID);
        bool CancelOrder(Order order, List<Product> products);
    }
}
