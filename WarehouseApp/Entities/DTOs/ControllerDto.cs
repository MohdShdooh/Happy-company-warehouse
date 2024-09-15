using Entities.Tools;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BaseResponse
    {
        public string Message { get; set; } = "Success";
        public ErrorCodes ErrorCode { get; set; } = ErrorCodes.InternalException;
        public bool Result => ErrorCode == ErrorCodes.Success;
    }

    #region RequestDTOs

    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }

    }
    public class UpdateUserRequest : CreateUserRequest
    {
        public int Id { get; set; } 

    }

    public class DeleteUserRequest 
    {
        public int Id { get; set; }
    }
    public class ChangePasswordRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
    public class GetUserRequest
    {
        public int Id { get; set; }
    }


    public class CreateWareHouseRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
    public class CreateItemRequest
    {
        public string Name { get; set; } 

        public string? SKUCode { get; set; }

        public int Quantity { get; set; }

        public double Cost { get; set; }

        public double? MSRPPrice { get; set; }

        public int WarehouseId { get; set; } 

    }
    public class GetItemsRequest
    {
        public int WarehouseId { get; set; } 
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    #endregion

    #region ResponseDTOs
    public class CreateUserResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InvalidEmail:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
    }
    public class GetUsersResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
       public List<UserDto> Users = new List<UserDto>();
    }
    public class UpdateUserResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InvalidEmail:
                case ErrorCodes.Faild:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
        public UserDto User = new UserDto();
    }
    public class DeleteUserResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
    }

    public class SignInResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.InvalidEmail:
                    return 401;
                case ErrorCodes.Faild:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
        public UserDto User = new UserDto();
        public string Token { get; set; }

    }
    public class ChangePasswordResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
    }
    public class CreateWareHouseResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.DoublicateWareHouse:
                case ErrorCodes.InValidWareHouse:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
    }
    public class GetWareHousesResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.DoublicateWareHouse:
                case ErrorCodes.InValidWareHouse:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
       public  List<WareHouseDto> wareHouses = new List<WareHouseDto>();
    }
    public class CreateItemResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.DoublicateItem:
                case ErrorCodes.InvalidItem:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
    }
    public class GetItemsResponse : BaseResponse
    {
        public int getHttpErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.Success:
                    return 200;
                case ErrorCodes.Faild:
                    return 202;
                case ErrorCodes.DoublicateItem:
                case ErrorCodes.InvalidItem:
                    return 400;
                case ErrorCodes.InternalException:
                default:
                    return 500;
            }
        }
       public List<ItemDto> items = new List<ItemDto>();
    }
    #endregion
}
