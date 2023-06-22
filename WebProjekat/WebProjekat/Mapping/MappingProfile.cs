﻿using AutoMapper;
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
        }
    }
}
