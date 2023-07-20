using AutoMapper;
using ProductOrderAPI.DTO.OrderDTO;
using ProductOrderAPI.DTO.ProductDTO;
using ProductOrderAPI.Models;

namespace ProductOrderAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderDetailsDTO>().ReverseMap();
            CreateMap<Product, OrderItemDetailsDTO>().ReverseMap();
        }
    }
}
