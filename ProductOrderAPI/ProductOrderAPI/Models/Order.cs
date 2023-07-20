using System.Collections.Generic;
using System;
using ProductOrderAPI.Common;

namespace ProductOrderAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string Comment { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime TimeOfOrder { get; set; }
        public List<OrderItem> OrderedProducts { get; set; }
        public DateTime DeliveryTime { get; set; }
        public EOrderStatus OrderStatus { get; set; }
        public double Price { get; set; }
    }
}
