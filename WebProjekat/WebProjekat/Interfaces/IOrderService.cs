using System.Collections.Generic;
using WebProjekat.DTO.OrderDTO;

namespace WebProjekat.Interfaces
{
    public interface IOrderService
    {
        bool NewOrder(OrderDTO order);
        bool CancelOrder(int orderID, string customerID, out string message);
        OrderDetailsDTO GetOrder(int orderID, out string message);
        OrderDetailsDTO GetOrderSeller(int orderID, string sellerID, out string message);
        List<OrderDTO> GetDeliveredOrders(string customerID);
        List<OrderDTO> GetPendingOrders(string customerID);
        List<OrderDTO> GetCanceledOrders(string customerID);
        List<OrderDTO> GetOrders();
        List<OrderDTO> GetDeliveredBySeller(string sellerID);
        List<OrderDTO> GetPendingBySeller(string sellerID);
        List<OrderDTO> GetCanceledSeller(string sellerID);
    }
}
