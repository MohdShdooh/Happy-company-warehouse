using Azure.Core;
using BAL.Interfaces;
using DAL.Entities;
using DAL.Repositories.Items;
using DAL.Repositories.WareHouse;
using Entities.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ItemsService : IItemService
    {
        private readonly ILogger<ItemsService> _logger;
        private readonly IItemRepository _itemRepo;

        public ItemsService(ILogger<ItemsService> logger, IItemRepository itemRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
        }
        public async Task<CreateItemResponse> CreateNewItem(CreateItemRequest request)
        {
            try
            {
                CreateItemResponse response = new CreateItemResponse();

                if (isValidItem(request))
                {
                    var result = _itemRepo.CreateNewItem(request).Result;
                    if (result)
                    {
                        response.ErrorCode = Entities.Tools.ErrorCodes.Success; 
                        response.Message = "Item Added Successfully ";
                        return response;
                    }
                    response.ErrorCode = Entities.Tools.ErrorCodes.DoublicateItem;
                    response.Message = "Item Already Exist .";
                    return response;
                }
                response.ErrorCode = Entities.Tools.ErrorCodes.InvalidItem;
                response.Message = "Please Fill all Requierd Information ."; 
                return response;

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while ItemService/CreateNewItem on : ", request.Name);
                throw ex;
            }
        }
        public async Task<GetItemsResponse> GetItemsByWareHouseId(GetItemsRequest request)
        {
            try
            {
                GetItemsResponse response = new GetItemsResponse();
                response.ErrorCode = Entities.Tools.ErrorCodes.Success;

                var result = _itemRepo.GetItemsByWareHouseId(request.WarehouseId, request.PageNumber, request.PageSize).Result;
                if (result != null && result.Count > 0 )
                {
                    response.items = result;
                    return response;
                }
                response.Message = "There are No Items yet .";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while ItemService/GetItemsByWareHouseId For wareHouseId =  : ", request.WarehouseId);
                throw ex;
            }
        }
        public async Task<GetItemsResponse> GetTopItems(int userID)
        {
            try
            {
                GetItemsResponse response = new GetItemsResponse();
                response.ErrorCode = Entities.Tools.ErrorCodes.Success;

                var result = _itemRepo.GetTopItems(userID).Result;
                if (result != null && result.Count > 0)
                {
                    response.ErrorCode = Entities.Tools.ErrorCodes.Success;
                    response.items = result;
                    return response;
                }
                response.Message = "There are No Items yet .";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while ItemService/GetLessItems For user =  : ", userID);
                throw ex;
            }
        }
        public async Task<GetItemsResponse> GetLessItems(int userID)
        {
            try
            {
                GetItemsResponse response = new GetItemsResponse();
                response.ErrorCode = Entities.Tools.ErrorCodes.Success;

                var result = _itemRepo.GetLessItems(userID).Result;
                if (result != null && result.Count > 0)
                {
                    response.ErrorCode = Entities.Tools.ErrorCodes.Success;
                    response.items = result;
                    return response;
                }
                response.Message = "There are No Items yet .";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while ItemService/GetLessItems For user =  : ", userID);
                throw ex;
            }
        }
        #region Private Methods
        private static bool isValidItem(CreateItemRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name) &&
                request.Quantity > 0  &&
                request.Cost > 0.0  &&
               request.WarehouseId > 0 )
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
