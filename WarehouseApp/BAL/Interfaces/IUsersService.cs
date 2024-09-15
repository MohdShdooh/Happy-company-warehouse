using DAL.Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUsersService
    {
        public Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        public Task<GetUsersResponse> GetAllUsers();
        public Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
        public Task<DeleteUserResponse> DeleteUserById(DeleteUserRequest request);
        public Task<Users> GetUserByEmail(string email);
        public Task<Users> GetUserById(int id );
        public Task<ChangePasswordResponse> ChangePasswordById(ChangePasswordRequest request);
    }
}
