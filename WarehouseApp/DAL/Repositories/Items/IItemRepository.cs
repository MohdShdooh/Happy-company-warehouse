using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Items
{
    public interface IItemRepository
    {
        public Task<bool> CreateNewItem(CreateItemRequest request);
        public Task<List<ItemDto>> GetItemsByWareHouseId(int wareHouseID, int pageNumber, int pageSize);
        public Task<List<ItemDto>> GetTopItems(int userID);
        public Task<List<ItemDto>> GetLessItems(int userID);
    }
}
