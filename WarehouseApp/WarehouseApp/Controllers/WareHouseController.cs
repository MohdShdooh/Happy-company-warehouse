using BAL.Interfaces;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace WarehouseApp.Controllers
{
    [ApiController]
    [Route("api/WareHouse")]
    [Authorize]
    public class WareHouseController : Controller
    {
        private readonly ILogger<WareHouseController> _logger;
        private readonly IWareHouseServicecs _wareHouseServicecs;
        private readonly IWebHostEnvironment _env;

        public WareHouseController(ILogger<WareHouseController> logger, IWareHouseServicecs wareHouseServicecs, IWebHostEnvironment env)
        { 
            _logger = logger;
            _wareHouseServicecs = wareHouseServicecs;
            _env = env;
        }

        /// <summary>
        /// Create new wareHouse
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Created wareHouse SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("CreateWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateWareHouseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateWareHouseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult CreateWareHouse(CreateWareHouseRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var response = _wareHouseServicecs.CreateWareHouse(request, userId).Result;
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
        /// Get WareHouses For Specific user
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200"> wareHouses gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetWareHousesByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWareHousesResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetWareHousesResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetWareHousesByUserID()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var response = _wareHouseServicecs.GetWareHousesByUserID(userId).Result;
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
        /// Get All Countries 
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200"> Countries gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetAllCountries")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetAllCountries()
        {
            try
            {
                var filePath = Path.Combine(_env.WebRootPath, "Countries.json");
                var jsonData = System.IO.File.ReadAllText(filePath);

                var countries = JsonConvert.DeserializeObject<List<CountriesDto>>(jsonData);
                var countryNames = countries.Select(c => c.name).ToList();

                return Ok(countryNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Oops!! There is a Technical issue . Please Try Again later .");
            }
        }
    }
}
