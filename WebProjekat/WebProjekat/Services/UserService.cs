using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using WebProjekat.Common;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationSection _secretKey;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _secretKey = config.GetSection("SecretKey");
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public TokenDTO CreateUser(RegisterUserDTO newUser, out string message)
        {
            TokenDTO token = null;
            message = "";
            if (!newUser.Password.Equals(newUser.PasswordConfirm))
            {
                message = "Passwords do not match";
                return token;
            }
            
            var user = _userRepository.GetUser(newUser.Email);
            if (newUser.UserType == EUserType.ADMIN)
            {
                message = "You cannot register as an admin";
                return token;
            }

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            switch (newUser.UserType)
            {
                case EUserType.CUSTOMER:
                    _userRepository.AddUser(_mapper.Map<Customer>(newUser));

                    break;
                case EUserType.SELLER:
                    var seller = _mapper.Map<Seller>(newUser);
                    seller.Approved = ESellerStatus.IN_PROCESS;
                    _userRepository.AddUser(seller);

                    break;
            }

            token = new TokenDTO()
            {
                Token = CreateToken(newUser.UserType.ToString(), newUser.Email),
                UserType = newUser.UserType
            };
            return token;
        }

        public UserInfoDTO GetInfo(string email)
        {
            var user = _userRepository.GetUser(email);

            if (user == null)
                return null;

            return _mapper.Map<UserInfoDTO>(user);
        }

        public TokenDTO LogInUser(LogInUserDTO user)
        {
            throw new System.NotImplementedException();
        }

        private string CreateToken(string userType, string email)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, userType));
            claims.Add(new Claim(ClaimTypes.Name, email));
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:44365", //url servera koji je izdao token
                claims: claims, //claimovi
                expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                signingCredentials: signinCredentials //kredencijali za potpis
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
