using System.Collections.Generic;
using WebProjekat.DTO.OrderDTO;

namespace WebProjekat.Interfaces
{
    public interface IOrderService
    {
        bool NewOrder(OrderDTO order);
        bool CancelOrder(int orderID, string customerID, out string message);
        OrderDetailsDTO GetOrder(int orderID, string customerID, out string message);
        List<OrderDTO> GetDeliveredOrders(string customerID);
        List<OrderDTO> GetPendingOrders(string customerID);
        List<OrderDTO> GetCanceledOrders(string customerID);
        List<OrderDTO> GetOrders();
    }
}
