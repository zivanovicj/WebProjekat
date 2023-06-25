using System.Collections.Generic;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
    public interface IOrderRepository
    {
        bool NewOrder(Order order, List<Product> products);
    }
}
