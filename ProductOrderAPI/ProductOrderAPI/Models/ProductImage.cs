namespace ProductOrderAPI.Models
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
