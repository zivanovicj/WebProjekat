using AutoMapper;
using System.Collections.Generic;
using WebProjekat.Common;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Infrastructure;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public List<UserInfoDTO> GetCustomers()
        {
            var customers = _mapper.Map<List<Customer>>(_adminRepository.GetCustomers());
            return _mapper.Map<List<UserInfoDTO>>(customers);
        }

        public List<UserInfoDTO> GetSellers()
        {
            var sellers = _mapper.Map<List<Seller>>(_adminRepository.GetSellers());
            return _mapper.Map<List<UserInfoDTO>>(sellers);
        }

        public bool SetSellerStatus(string email, ESellerStatus status)
        {
            var seller = (Seller)_adminRepository.GetSeller(email);
            if (seller == null || seller.Approved != ESellerStatus.IN_PROCESS)
                return false;
            seller.Approved = status;
            _adminRepository.SetSellerStatus(seller);
            return true;
        }
    }
}
