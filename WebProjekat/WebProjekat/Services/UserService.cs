﻿using AutoMapper;
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

        public bool ChangePassword(ChangePasswordDTO data, out string message)
        {
            if (!data.NewPassword.Equals(data.NewPasswordConfirm))
            {
                message = "New password doesn't match";
                return false;
            }

            var user = _userRepository.GetUser(data.Email);
            if(user == null)
            {
                message = "User doesn't exist";
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(data.OldPassword, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
                _userRepository.UpdateUser(user);
                message = "Password updated";
                return true;
            }
            else
            {
                message = "Incorrect password";
                return false;
            }
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

        public TokenDTO GoogleLogInUser(GoogleLogInDTO newUser)
        {
            var user = _userRepository.GetUser(newUser.Email);
            if(user == null)
                _userRepository.AddUser(_mapper.Map<Customer>(newUser));
            if (user.UserType != EUserType.CUSTOMER)
                return null;

            TokenDTO token = new TokenDTO()
            {
                Token = CreateToken(EUserType.CUSTOMER.ToString(), newUser.Email),
                UserType = EUserType.CUSTOMER
            };
            return token;
        }

        public TokenDTO LogInUser(LogInUserDTO user)
        {
            TokenDTO token = new TokenDTO();

            var userInfo = _userRepository.GetUser(user.Email);
            if (userInfo == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(user.Password, userInfo.Password))
            {
                token.Token = CreateToken(userInfo.UserType.ToString(), user.Email);
                token.UserType = userInfo.UserType;
            }
            return token;
        }

        public bool UpdateUser(UpdateUserDTO user)
        {
            var userInfo = _userRepository.GetUser(user.Email);
            if(userInfo == null)
                return false;

            userInfo.Username = user.Username;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.DateOfBirth = user.DateOfBirth;
            userInfo.Address = user.Address;
            _userRepository.UpdateUser(userInfo);

            return true;
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