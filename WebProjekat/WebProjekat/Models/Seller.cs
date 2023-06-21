using System.Collections.Generic;
using WebProjekat.Common;

namespace WebProjekat.Models
{
    public class Seller:User
    {
        public ESellerStatus Approved { get; set; }
        public List<Product> Products { get; set; }
    }
}
