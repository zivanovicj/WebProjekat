using ProductOrderAPI.Common;
using System.Collections.Generic;
using System;

namespace ProductOrderAPI.DTO.OrderDTO
{
    public class OrderDetailsDTO
    {
        public string? CustomerID { get; set; }
        public string Comment { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? TimeOfOrder { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public EOrderStatus? OrderStatus { get; set; }
        public double? Price { get; set; }
        public List<OrderItemDetailsDTO> Products { get; set; }
    }
}
