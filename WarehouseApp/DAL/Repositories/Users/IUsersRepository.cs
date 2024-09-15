

using Entities.DTOs;

namespace DAL.Repositories.Users
{
    public interface IUsersRepository
    {
        public Task<bool> CreateUser(CreateUserRequest request);
        public  Task<List<UserDto>> GetAllUsers();
        public Task<UserDto> UpdateUser(UpdateUserRequest request);
        public Task<bool> DeleteUserById(int id);
        public Task<Entities.Users> GetUserByEmail(string email);
        public  Task<Entities.Users> GetUserById(int id);
        public Task<bool> ChangePasswordById(ChangePasswordRequest request);
    }
}
