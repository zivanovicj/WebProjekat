using AutoMapper;
using WebProjekat.DTO.OrderDTO;
using WebProjekat.DTO.ProductDTO;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Models;

namespace WebProjekat.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<Admin, RegisterUserDTO>().ReverseMap();
            CreateMap<Customer, RegisterUserDTO>().ReverseMap();
            CreateMap<Seller, RegisterUserDTO>().ReverseMap();
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<Admin, UserInfoDTO>().ReverseMap();
            CreateMap<Customer, UserInfoDTO>().ReverseMap();
            CreateMap<Seller, UserInfoDTO>().ReverseMap();
            CreateMap<Customer, GoogleLogInDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
