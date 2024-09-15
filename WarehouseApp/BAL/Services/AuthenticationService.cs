using DAL.Entities;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationService(ILogger<AuthenticationService> logger, IConfiguration configuration) 
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<UserDto> AuthenticateUser(Users user , SignInRequest request)
        {
            try
            {
                
                Users AdminUser = _configuration.GetSection("AdminUser").Get<Users>();
                if (AdminUser.Email.Equals(request.Email))
                {
                    if (!AdminUser.Password.Equals(request.Password))
                    {
                        return null;
                    }
                    UserDto userDto = new UserDto()
                    {
                        Id = AdminUser.Id,
                        Name = AdminUser.Name,
                        Email = AdminUser.Email,
                        Role = (UserRole)AdminUser.Role,
                        UserStatus = (UserStatus)AdminUser.Role
                    };
                    return userDto;
                }

               // we hash the request password because we can't decrypt the hashed password . 
               // because the hashing algo is one way hashing .
                if (PasswordHasher.HashPassword(request.Password).Equals(user.Password) && user.Status == (int)UserStatus.Active)
                {
                    UserDto userDto = new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Role = (UserRole) user.Role,
                        UserStatus = (UserStatus) user.Role
                    };
                    return userDto;
                }
                return null;

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while AuthenticationService /AuthenticateUser user  : ", user.Email);
                throw ex;
            }

        }

    }
}
