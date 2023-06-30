namespace WebProjekat.Models
{
    public class UserImage
    {
        public int UserImageID { get; set; }
        public string UserID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
