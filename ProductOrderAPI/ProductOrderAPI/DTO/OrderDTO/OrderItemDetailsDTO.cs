namespace ProductOrderAPI.DTO.OrderDTO
{
    public class OrderItemDetailsDTO
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
