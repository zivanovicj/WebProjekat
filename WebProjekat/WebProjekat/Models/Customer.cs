using System.Collections.Generic;

namespace WebProjekat.Models
{
    public class Customer:User
    {
        public List<Order> Orders { get; set; }
    }
}
