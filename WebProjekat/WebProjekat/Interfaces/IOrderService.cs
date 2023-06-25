using WebProjekat.DTO.OrderDTO;

namespace WebProjekat.Interfaces
{
    public interface IOrderService
    {
        bool NewOrder(OrderDTO order);
    }
}
