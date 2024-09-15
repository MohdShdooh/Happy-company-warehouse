using BAL.Interfaces;
using Entities.DTOs;
using DAL.Repositories.Users;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using DAL.Entities;
using Entities.Tools;
using Microsoft.Extensions.Configuration;
namespace BAL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository ;
        private readonly ILogger<UsersService> _logger ;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger , IConfiguration configuration) 
        {
            _usersRepository = usersRepository ;
            _logger = logger ;
            _configuration = configuration ;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            try
            {
                CreateUserResponse resposne = new CreateUserResponse();
                if (!string.IsNullOrEmpty(request.Email) && IsValidEmail(request.Email))
                {
                    if (string.IsNullOrEmpty(request.Name))
                    {
                        resposne.ErrorCode = Entities.Tools.ErrorCodes.Faild;
                        resposne.Message = "The Name is requierd .";
                        return resposne;
                    }

                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        request.Password = PasswordHasher.HashPassword(request.Password);
                        var result =  _usersRepository.CreateUser(request).Result;
                        if(!result)
                        {
                            resposne.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                            resposne.Message = "The Email Already Exist! Try Another one .";
                            return resposne;
                        }
                        resposne.ErrorCode = Entities.Tools.ErrorCodes.Success;
                        resposne.Message = "The Account created successfully .";
                        return resposne;
                    }
                    resposne.ErrorCode = Entities.Tools.ErrorCodes.Faild;
                    resposne.Message = "Please Fill the Password .";
                    return resposne;
                }
                resposne.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                resposne.Message = "Invalid Email ! Try Another one .";
                return resposne;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user with Email: {@Request}", request.Email);
                throw ex; 
            }
        }

        public async Task<GetUsersResponse> GetAllUsers()
        {
            try
            {
                GetUsersResponse response = new GetUsersResponse();
                // i add a Admin user will not be deleted and it's default data that added to appsettings . 
                // also, i'll return it in the first el of list always .
                List<UserDto> users = new List<UserDto>() { _configuration.GetSection("AdminUser").Get<UserDto>() };
                
                var result = _usersRepository.GetAllUsers().Result;
                users.AddRange(result);

                if (result != null && result.Count > 0 )
                {
                    response.ErrorCode = Entities.Tools.ErrorCodes.Success;
                    response.Users = users;
                    return response;
                }
                response.ErrorCode = Entities.Tools.ErrorCodes.Success;
                response.Message = "There are no Users yet except the Admin.";
                response.Users = users; 
                return response;

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users ");
                throw ex;
            }
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                UpdateUserResponse response = new UpdateUserResponse();
                if (!string.IsNullOrEmpty(request.Email) && IsValidEmail(request.Email))
                {
                    if (string.IsNullOrEmpty(request.Name))
                    {
                        response.ErrorCode = Entities.Tools.ErrorCodes.Faild;
                        response.Message = "The Name is requierd .";
                        return response;
                    }
                    var result = _usersRepository.UpdateUser(request).Result;
                    if (result != null)
                    {
                        response.ErrorCode = Entities.Tools.ErrorCodes.Success;
                        response.User = result;
                        return response;
                    }

                    response.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                    response.Message = "Another user have this email. please Try another one";
                    return response;
                }
                response.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                response.Message = "Invalid Email ! Try Another one .";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the  user : ", request.Id);
                throw ex;
            }
        }
        public async Task<DeleteUserResponse> DeleteUserById(DeleteUserRequest request)
        {
            try
            {
                DeleteUserResponse response = new DeleteUserResponse();
                
                // this is for check if the user is Admin or not . 
                // if the user is Admin we cann't delete it .
                var user = _usersRepository.GetUserById(request.Id).Result;
                if (user.Role == (int)UserRole.Admin)
                {
                    response.ErrorCode = ErrorCodes.Faild;
                    response.Message = "you can not delete an Admin User .";
                    return response;
                }
                var result = _usersRepository.DeleteUserById(request.Id).Result;
                if(result)
                {
                    response.ErrorCode = ErrorCodes.Success;
                    response.Message = "User Deleted Successfully ";
                    return response;
                }
                response.ErrorCode = Entities.Tools.ErrorCodes.Faild;
                response.Message = "User Not Found";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the  user : ", request.Id);
                throw ex;
            }

        }

        public async Task<Users> GetUserByEmail(string email)
        {
            try
            {
                var user = _usersRepository.GetUserByEmail(email).Result;
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getUser by it's Email : ", email);
                throw ex;
            }
        }

        public async Task<Users> GetUserById(int id)
        {
            try
            {
                var user = _usersRepository.GetUserById(id).Result;
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getUser by it's id : ", id);
                throw ex;
            }
        }
        public async Task<ChangePasswordResponse> ChangePasswordById(ChangePasswordRequest request)
        {
            try
            {
                // this condition will prevent from excute for the admin user that found in the appsettings .
                // so, and will prevent to excute without user id . 
                // also, if the password empty we will not excute it .
                if (!string.IsNullOrEmpty(request.Password) && request.Id > 0 )
                {
                    request.Password = PasswordHasher.HashPassword(request.Password);
                    var result = _usersRepository.ChangePasswordById(request).Result;
                    if (result)
                    {
                        return new ChangePasswordResponse() { ErrorCode = ErrorCodes.Success, Message = "Password changed Successfully " }; 
                    }
                }
                return new ChangePasswordResponse() { ErrorCode = ErrorCodes.Faild, Message = "Faild to Change Password. " };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while usersService/ChangePasswordById by it's id : ", request.Id);
                throw ex;
            }
        }



        #region Private Methods 
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        #endregion 
    }
}
