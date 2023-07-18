using System.Collections.Generic;
using UserAdminAPI.Common;

namespace UserAdminAPI.Models
{
    public class Seller : User
    {
        public ESellerStatus Approved { get; set; }
    }
}
