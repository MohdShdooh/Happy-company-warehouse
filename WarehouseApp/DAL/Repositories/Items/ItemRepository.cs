using Azure.Core;
using DAL.DBContext;
using DAL.Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Items
{
    public class ItemRepository : IItemRepository
    {
        private readonly WareHouseContext _context;
        private readonly DbSet<Entities.Item> _dbSet;
        private readonly ILogger<ItemRepository> _logger;
        public ItemRepository(WareHouseContext context, ILogger<ItemRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Entities.Item>();
            _logger = logger ?? throw new ArgumentNullException();
        }
        public async Task<bool> CreateNewItem(CreateItemRequest request)
        {
            try
            {
                _logger.LogInformation("ItemRepository/CreateNewItem method Started ");
                Entities.Item item = new Entities.Item()
                {
                    Name = request.Name,
                    Cost = request.Cost,
                    MSRPPrice = request.MSRPPrice,
                    Quantity = request.Quantity,
                    SKUCode = request.SKUCode,
                    WarehouseId = request.WarehouseId
                };
                var query = _dbSet.Add(item);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogWarning("Faild to add a duplicate item with Name: {Email}. Unique constraint violation.", request.Name);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating item with Name: {@Request}", request.Name);
                throw ex;
            }
        }
        public async Task<List<ItemDto>> GetItemsByWareHouseId(int wareHouseID, int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("ItemRepository/GetItemsByWareHouseId method Started ");

                var itemsQuery = _context.Items
                                         .Where(item => item.WarehouseId == wareHouseID)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize);

                var items = await itemsQuery.ToListAsync();

                var itemDtos = items.Select(item => new ItemDto
                {
                    Id = item.Id,
                    SKUCode = item.SKUCode,
                   Cost = item.Cost,
                   MSRPPrice = item.MSRPPrice,
                   Name = item.Name,
                   Quantity = item.Quantity 
                }).ToList();

                return itemDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching item For warehouseId = ", wareHouseID);
                throw ex;
            }
        }
        public async Task<List<ItemDto>> GetTopItems(int userID)
        {
            try
            {
                _logger.LogInformation("ItemRepository/GetTopItems method Started ");

                var itemsQuery = await _context.Items
                                    .Where(i => i.Warehouse.UserId == userID) 
                                    .OrderByDescending(i => i.Quantity) 
                                    .Take(10) 
                                    .ToListAsync();
                
                var itemDtos = itemsQuery.Select(item => new ItemDto
                {
                    Id = item.Id,
                    SKUCode = item.SKUCode,
                    Cost = item.Cost,
                    MSRPPrice = item.MSRPPrice,
                    Name = item.Name,
                    Quantity = item.Quantity
                }).ToList();

                return itemDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching item For user = ", userID);
                throw ex;
            }
        }
        public async Task<List<ItemDto>> GetLessItems(int userID)
        {
            try
            {
                _logger.LogInformation("ItemRepository/GetLessItems method Started ");

                var itemsQuery = await _context.Items
                                    .Where(i => i.Warehouse.UserId == userID)
                                    .OrderBy(i => i.Quantity)
                                    .Take(10)
                                    .ToListAsync();

                var itemDtos = itemsQuery.Select(item => new ItemDto
                {
                    Id = item.Id,
                    SKUCode = item.SKUCode,
                    Cost = item.Cost,
                    MSRPPrice = item.MSRPPrice,
                    Name = item.Name,
                    Quantity = item.Quantity
                }).ToList();

                return itemDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching item For user = ", userID);
                throw ex;
            }
        }
    }
}
