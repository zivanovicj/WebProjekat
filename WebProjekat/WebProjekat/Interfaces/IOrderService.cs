using WebProjekat.DTO.OrderDTO;

namespace WebProjekat.Interfaces
{
    public interface IOrderService
    {
        bool NewOrder(OrderDTO order);
        bool CancelOrder(int orderID, string customerID, out string message);
        OrderDetailsDTO GetOrder(int orderID, string customerID, out string message);
    }
}
