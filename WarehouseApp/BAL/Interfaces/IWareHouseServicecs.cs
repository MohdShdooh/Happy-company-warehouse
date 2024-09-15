using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IWareHouseServicecs
    {
        public Task<CreateWareHouseResponse> CreateWareHouse(CreateWareHouseRequest request, int userID);
        public Task<GetWareHousesResponse> GetWareHousesByUserID(int userID);
    }
}
