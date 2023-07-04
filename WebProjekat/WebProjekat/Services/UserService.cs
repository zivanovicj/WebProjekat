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
using Microsoft.AspNetCore.Http;
using System.IO;
using WebProjekat.Repository;

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

        public void AddUserImage(string userID, IFormFile file)
        {
            UserImage image = new() { ImageTitle = file.FileName, UserID = userID };

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            image.ImageData = ms.ToArray();
            ms.Close();
            ms.Dispose();

            _userRepository.AddUserImage(image);
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
            string role = "";
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
            role = newUser.UserType.ToString();

            switch (newUser.UserType)
            {
                case EUserType.CUSTOMER:
                    _userRepository.AddUser(_mapper.Map<Customer>(newUser));

                    break;
                case EUserType.SELLER:
                    role = "UNVERIFIED";
                    var seller = _mapper.Map<Seller>(newUser);
                    seller.Approved = ESellerStatus.IN_PROCESS;
                    _userRepository.AddUser(seller);

                    break;
            }

            token = new TokenDTO()
            {
                Token = CreateToken(role, newUser.Email),
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

        public UserImage GetUserImage(string userID)
        {
            return _userRepository.GetUserImage(userID);
        }

        public TokenDTO GoogleLogInUser(GoogleLogInDTO newUser)
        {
            var user = _userRepository.GetUser(newUser.Email);
            if (user == null)
            {
                var customer = _mapper.Map<Customer>(newUser);
                customer.UserType = EUserType.CUSTOMER;
                _userRepository.AddUser(customer);
            }

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

            string role = userInfo.UserType.ToString();

            if (BCrypt.Net.BCrypt.Verify(user.Password, userInfo.Password))
            {
                if (userInfo.UserType == EUserType.SELLER)
                {
                    var seller = (Seller)userInfo;
                    if (seller.Approved != ESellerStatus.VERIFIED)
                        role = "UNVERIFIED";
                }
                token.Token = CreateToken(role, user.Email);
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

        public bool UpdateUserImage(IFormFile file, string userID)
        {
            var userImage = _userRepository.GetUserImage(userID);
            if (userImage == null) return false;
            if (userImage.UserID != userID) return false;

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            userImage.ImageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            _userRepository.UpdateUserImage(userImage);
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
