using BAL.Interfaces;
using DAL.Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WarehouseApp.Controllers
{
    [ApiController]
    [Route("api/Items")]
    [Authorize]
    public class ItemsController : Controller
    {

        private readonly ILogger<WareHouseController> _logger;
        private readonly IItemService _itemService;

        public ItemsController(ILogger<WareHouseController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        /// <summary>
        /// Create new Item
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Created Item SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost("CreateNewItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateItemResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateItemResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult CreateNewItem(CreateItemRequest request)
        {
            try
            {
                var response = _itemService.CreateNewItem(request).Result;
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
        /// Get Items For Specific WareHouse
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200"> Items gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetItemsByWareHouseId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetItemsByWareHouseId([FromQuery] GetItemsRequest request)
        {
            try
            {
                var response = _itemService.GetItemsByWareHouseId(request).Result;
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
        /// Get Top 10 Items For Specific user
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200"> Items gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetTopItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetTopItems()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var response = _itemService.GetTopItems(userId).Result;
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
        /// Get Less 10 Items For Specific user
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <response code="200"> Items gotten SuccessFully </response>
        /// <response code="400">Missed parameter </response>
        /// <response code="401">unAuthrized</response>
        /// <response code="500">Internal Server error</response>
        [HttpGet("GetLessItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetItemsResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        public IActionResult GetLessItems()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var response = _itemService.GetLessItems(userId).Result;
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
    }
}
