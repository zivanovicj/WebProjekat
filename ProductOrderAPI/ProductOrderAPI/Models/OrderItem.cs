using Org.BouncyCastle.Asn1.X509;

namespace ProductOrderAPI.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
    }
}
