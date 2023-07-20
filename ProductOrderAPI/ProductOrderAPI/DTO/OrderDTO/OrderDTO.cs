using ProductOrderAPI.Common;
using System.Collections.Generic;
using System;

namespace ProductOrderAPI.DTO.OrderDTO
{
    public class OrderDTO
    {
        public int? OrderID { get; set; }
        public string? CustomerID { get; set; }
        public string Comment { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? TimeOfOrder { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public EOrderStatus? OrderStatus { get; set; }
        public double? Price { get; set; }
        public List<OrderItemDTO> OrderedProducts { get; set; }
    }
}
