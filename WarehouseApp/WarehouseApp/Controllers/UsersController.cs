using BAL.Interfaces;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WarehouseApp.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService) 
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Create a new User  
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="201">User Created SuccessFully </response>
        /// <response code="400">Invalid Email </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = _usersService.CreateUser(request).Result;
                if (response.ErrorCode == Entities.Tools.ErrorCodes.Success)
                {
                    return Ok(response);
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }

        /// <summary>
        /// Get all Users  
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Users are gotten SuccessFully </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetAllUsers()
        {
            try
            {
                // don't forget to add the admin user to be the el in the list .
                
                var response = _usersService.GetAllUsers().Result;
                if (response.ErrorCode == Entities.Tools.ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }

        /// <summary>
        /// Update Specific User
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">User is updated SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUserResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                var response = _usersService.UpdateUser(request).Result;
                if (response.ErrorCode == Entities.Tools.ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }

        /// <summary>
        /// Delete Specific User
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">User is Deleted SuccessFully </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteUserResponse))]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteUserResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult DeleteUserById([FromBody] DeleteUserRequest request)
        {
            try
            {
                var response = _usersService.DeleteUserById(request).Result;
                if (response.ErrorCode == Entities.Tools.ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }

        /// <summary>
        /// Update Specific User
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">User is updated SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("ChangePasswordById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangePasswordResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ChangePasswordResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult ChangePasswordById([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var response = _usersService.ChangePasswordById(request).Result;
                if (response.ErrorCode == Entities.Tools.ErrorCodes.Success)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(response.getHttpErrorCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }

        /// <summary>
        /// Get Specific User
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">User is gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangePasswordResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ChangePasswordResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetUserById([FromQuery] GetUserRequest request)
        {
            try
            {
                var response = _usersService.GetUserById(request.Id).Result;
                if (response != null)
                {
                    return Ok(JsonConvert.SerializeObject(response));
                }
                return StatusCode(202, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }


    }
}
