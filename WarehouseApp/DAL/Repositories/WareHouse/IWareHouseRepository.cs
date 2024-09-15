using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.WareHouse
{
    public interface IWareHouseRepository
    {
        public Task<bool> CreateWareHouse(CreateWareHouseRequest request, int userID);
        public Task<List<WareHouseDto>> GetWareHousesByUserID(int userID);

    }
}
