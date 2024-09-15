using BAL.Interfaces;
using BAL.Services;
using DAL.Entities;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;

namespace WarehouseApp.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthenticationController : Controller
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IUsersService _userService;
        private readonly AuthenticationService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;



        public AuthenticationController(JwtHelper jwtHelper, IUsersService userService, ILogger<AuthenticationController> logger, AuthenticationService authService, IConfiguration configuration)
        {
            _jwtHelper = jwtHelper;
            _userService = userService; 
            _logger = logger;
            _authService = authService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">User Authenticated SuccessFully </response
        /// <response code="500">Internal Server error</response>
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInResponse))]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SignInResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult SignIn([FromBody] SignInRequest request)
        {
            try
            {
                SignInResponse response = new SignInResponse();

                // we get the user depending on it's email . 
                // then in this stage we will check if the user not found we will tell the user it's cradential is wrong .
                // i a special case i add a configuration to read the setting to get the admin user . 
                var user = _userService.GetUserByEmail(request.Email).Result;
                var AdminUser = _configuration.GetSection("AdminUser").Get<Users>();
                
                if (user == null && !AdminUser.Email.Equals(request.Email))
                {
                    response.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                    response.Message = "Sign in Cradential not valid . please try again ";
                    return StatusCode(response.getHttpErrorCode(), response);
                }
                // we will check if the user authenicated we will generate token . 
                var authUser = _authService.AuthenticateUser(user, request).Result;
                if (authUser != null)
                {
                    response.ErrorCode = ErrorCodes.Success;
                    response.User = authUser; 
                    // Generate JWT token
                    response.Token = _jwtHelper.GenerateToken(authUser.Id);
                    return Ok(JsonConvert.SerializeObject(response));

                }else
                {
                    // if the user unAuthenticate we will check it's status is inActive we will tell him . 
                    // but if it's status (Active) and it's unAuthenticated he provid a invalid password .
                    if (!AdminUser.Email.Equals(request.Email) && user.Status != (int)UserStatus.Active)
                    {
                        response.ErrorCode = Entities.Tools.ErrorCodes.InActiveUser;
                        response.Message = "Your account is Pennding . please try again later";
                        return StatusCode(response.getHttpErrorCode(), response);
                    }

                    response.ErrorCode = Entities.Tools.ErrorCodes.InvalidEmail;
                    response.Message = "Sign in Cradential not valid . please try again ";
                    return StatusCode(response.getHttpErrorCode(), response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
           
        }
    }
}
