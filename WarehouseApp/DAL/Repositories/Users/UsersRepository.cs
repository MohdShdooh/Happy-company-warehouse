using Azure.Core;
using DAL.DBContext;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DAL.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WareHouseContext _context; 
        private readonly DbSet<Entities.Users>  _dbSet;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(WareHouseContext context, ILogger<UsersRepository> logger) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Entities.Users>();
            _logger = logger ?? throw new ArgumentNullException();
        }
        public async Task<bool> CreateUser(CreateUserRequest request)
        {
            try
            {
                _logger.LogInformation("CreateUser method Started with Email: {@Request}", request.Email);
               Entities.Users user = new Entities.Users()
                {
                    Name = request.Name,
                    Password = request.Password,
                    Email = request.Email,
                    Role = (int)request.Role,
                    Status = (int)request.Status,
                };

                var query = _dbSet.Add(user);
                var result = await _context.SaveChangesAsync();
                if (result > 0 )
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateException dbEx)
            {
                // will enter in this exception if the email already found because it's unieqe .
                // if the email exist i will catch this error and return false for retreive message to the user . 
                _logger.LogWarning("Faild to add a duplicate user with email: {Email}. Unique constraint violation.", request.Email);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user with Email: {@Request}", request.Email);
                throw ex;
            }

        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("GetAllUsers method Started ");
               var query = await _dbSet.Select(usr=> new UserDto
               {
                   Id = usr.Id,
                   Name = usr.Name,
                   Email = usr.Email,
                   Role = (UserRole)usr.Role,
                   UserStatus = (UserStatus)usr.Status,
               })
                    .ToListAsync();
                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user with Email: {@Request}");
                throw ex;
            }

        }
        public async Task<UserDto> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                _logger.LogInformation("updateUser Repository method Started with Email: {@Request}", request.Email);
                var oldUser = _dbSet.FirstOrDefault(usr=>usr.Id == request.Id);
                if (oldUser != null)
                {
                    oldUser.Name = request.Name;
                    oldUser.Email = request.Email;
                    oldUser.Status = (int) request.Status;
                    oldUser.Role = (int) request.Role;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        UserDto updatedUser = new UserDto()
                        {
                            Id = request.Id,
                            Name = request.Name,
                            Email = request.Email,
                            Role = (UserRole)request.Role,
                            UserStatus = (UserStatus)request.Status,
                        };
                        return updatedUser;
                    }
                }
                return null;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogWarning("Faild to add a duplicate user with email: {Email}. Unique constraint violation.", request.Email);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating  user with email : ", request.Email);
                throw ex;
            }
        }
        public async Task<bool> DeleteUserById(int id)
        {
            try
            {
                _logger.LogInformation("DeleteUserById Repository method Started for user }", id);
                var query = _dbSet.FirstOrDefault(usr=> usr.Id == id);
                if(query != null)
                {
                    _dbSet.Remove(query);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting  user  : ", id);
                throw ex;
            }
        }
        public async Task<Entities.Users> GetUserByEmail(string email)
        {
            try
            {
                _logger.LogInformation("GetUserByEmail Repository method Started for user }", email);
                var query = _dbSet.FirstOrDefault(usr=> usr.Email == email);
                if (query != null)
                {
                    return query;
                }
                return null;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while fetching user  : ", email);
                throw ex;
            }
        }

        public async Task<Entities.Users> GetUserById(int id)
        {
            try
            {
                _logger.LogInformation("GetUserById Repository method Started for user }", id);
                var query = _dbSet.FirstOrDefault(usr => usr.Id == id);
                if (query != null)
                {
                    return query;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user  : ", id);
                throw ex;
            }
        }
        public async Task<bool> ChangePasswordById(ChangePasswordRequest request)
        {
            try
            {
                _logger.LogInformation("ChangePasswordById Repository method Started for user }", request.Id);
                var query = _dbSet.FirstOrDefault(usr => usr.Id == request.Id);
                if (query != null)
                {
                    query.Password = request.Password;
                    await _context.SaveChangesAsync();  
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while change the password for user  : ", request.Id);
                throw ex;
            }

        }

    }
}
