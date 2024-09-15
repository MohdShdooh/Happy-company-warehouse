using BAL.Interfaces;
using DAL.Repositories.WareHouse;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class WareHouseService : IWareHouseServicecs
    {
        private readonly ILogger<WareHouseService> _logger;
        private readonly IWareHouseRepository _wareHouseRepo;

        public WareHouseService(ILogger<WareHouseService> logger, IWareHouseRepository wareHouseRepo)
        {
            _logger = logger;
            _wareHouseRepo = wareHouseRepo;
        }

        public async Task<CreateWareHouseResponse> CreateWareHouse(CreateWareHouseRequest request, int userID)
        {
            try
            {
                CreateWareHouseResponse response = new CreateWareHouseResponse();
                if (isValidWareHouse(request))
                {
                    var result = _wareHouseRepo.CreateWareHouse(request, userID).Result;
                    if (result)
                    {
                        response.ErrorCode = ErrorCodes.Success;
                        response.Message = "WareHouse Added Successfully . "; 
                        return response;
                    }
                    response.ErrorCode = ErrorCodes.DoublicateWareHouse;
                    response.Message = "wareHouse already Exist ."; 
                    return response;
                }
                response.ErrorCode = ErrorCodes.InValidWareHouse;
                response.Message = "wareHouse is invalid parameter . ";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while WareHouseService/CreateWareHouse wareHouse Name: ", request.Name);
                throw ex;
            }
        }
        public async Task<GetWareHousesResponse> GetWareHousesByUserID(int userID)
        {
            try
            {
                GetWareHousesResponse response = new GetWareHousesResponse();
                var result = _wareHouseRepo.GetWareHousesByUserID(userID).Result;
                if (result != null && result.Count > 0 )
                {
                    response.ErrorCode = ErrorCodes.Success;
                    response.wareHouses = result;
                    return response; 
                }
                response.Message = "There are no WareHouses Yet . Try To Add one ! ";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while WareHouseService/GetWareHousesByUserID wareHouse for user : ", userID);
                throw ex;
            }
        }


        #region Private Methods
        private static bool isValidWareHouse(CreateWareHouseRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name) &&
                !string.IsNullOrEmpty(request.Address) &&
                !string.IsNullOrEmpty(request.City) && 
                !string.IsNullOrEmpty(request.Country))
            {
                return true ;
            }
            return false;
        }
        #endregion
    }
}
