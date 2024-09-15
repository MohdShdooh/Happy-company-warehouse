using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IItemService
    {
        public Task<CreateItemResponse> CreateNewItem(CreateItemRequest request);
        public Task<GetItemsResponse> GetItemsByWareHouseId(GetItemsRequest request);
        public Task<GetItemsResponse> GetTopItems(int userID);
        public Task<GetItemsResponse> GetLessItems(int userID);


    }
}
