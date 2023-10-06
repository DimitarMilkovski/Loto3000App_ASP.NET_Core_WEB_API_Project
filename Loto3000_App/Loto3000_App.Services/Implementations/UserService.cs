using Loto3000_App.DataAcess.Implementations;
using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Loto3000_App.Domain.Enums;
using Loto3000_App.DTOs.UserDtos;
using Loto3000_App.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public string LogIn(LogInDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new Exception("Username and password are required fields!");
            }


            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();


            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginDto.Password);


            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);


            string hash = Encoding.ASCII.GetString(hashBytes);


            User userDb = _userRepository.LoginUser(loginDto.Username, hash);
            if (userDb == null)
            {
                throw new Exception("User not found! Wrong password or username!");
            }


            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("This is a very very very secret key!");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.Firstname} {userDb.Lastname}"),
                        new Claim(ClaimTypes.Role, userDb.Role.ToString())
                    }
                )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public void Register(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hash = Encoding.ASCII.GetString(hashBytes);

            User user = new User
            {
                Username = registerUserDto.Username,
                Firstname = registerUserDto.Firstname,
                Lastname = registerUserDto.Lastname,
                Password = hash,
                Role = RoleEnum.Player
            };
            _userRepository.Add(user);


        }






        public void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new Exception("Username and password are required!");
            }

            if (string.IsNullOrEmpty(registerUserDto.Firstname) || string.IsNullOrEmpty(registerUserDto.Lastname))
            {
                throw new Exception("Firstname and Lastname are required!");
            }

            if (registerUserDto.Username.Length > 30)
            {
                throw new Exception("Maximum length of username is 50 characters");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new Exception("Passwords must match");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new Exception($"The username {registerUserDto.Username} is already taken!");
            }
        }

    }
}
