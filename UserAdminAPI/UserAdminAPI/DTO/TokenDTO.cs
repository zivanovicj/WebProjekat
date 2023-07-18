using UserAdminAPI.Common;

namespace UserAdminAPI.DTO
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public EUserType UserType { get; set; }
    }
}
