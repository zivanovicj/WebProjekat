using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System;
using UserAdminAPI.Common;
using UserAdminAPI.DTO;
using UserAdminAPI.Interfaces;
using UserAdminAPI.Models;
using UserAdminAPI.Repository.Interfaces;

namespace UserAdminAPI.Services
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

            var vr = status.ToString();
            var apiKey = "E768E081003B29BF53422EC3597F30618F3E05C3A893FEFE1118B10338F1D41724E4A0488A9C06372E3A6819FCA263C0";
            var recipientEmail = email;
            var subject = "Status of verification";
            var bodyHtml = $"<h1>Hello, your status of verification is</h1><p>{vr}</p>";
            SendEmail(apiKey, recipientEmail, subject, bodyHtml);

            return true;
        }

        private static void SendEmail(string apiKey, string recipientEmail, string subject, string body)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.elasticemail.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();

                var requestContent = new StringContent(
                    $"apikey={Uri.EscapeDataString(apiKey)}&from=zivanovicj71@gmail.com&to=" +
                    $"{Uri.EscapeDataString(recipientEmail)}&subject={Uri.EscapeDataString(subject)}&bodyHtml=" +
                    $"{Uri.EscapeDataString(body)}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"
                );

                var response = client.PostAsync("email/send", requestContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email sent successfully!");
                }
                else
                {
                    var errorResponse = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Failed to send email. Error message: " + errorResponse);
                }
            }
        }
    }
}
