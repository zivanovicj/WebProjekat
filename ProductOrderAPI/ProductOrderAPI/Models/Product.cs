using System.Collections.Generic;

namespace ProductOrderAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string SellerID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
