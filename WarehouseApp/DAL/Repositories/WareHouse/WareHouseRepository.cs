using DAL.DBContext;
using Entities.DTOs;
using Entities.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.WareHouse
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly ILogger<WareHouseRepository> _logger;
        private readonly WareHouseContext _context;
        private readonly DbSet<Entities.Warehouse> _dbSet;

        public WareHouseRepository(WareHouseContext context,ILogger<WareHouseRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Entities.Warehouse>();
            _logger = logger ?? throw new ArgumentNullException();
        }

        public async Task<bool> CreateWareHouse(CreateWareHouseRequest request, int userID)
        {
            try
            {
                _logger.LogInformation("WareHouseRepository/CreateWareHouse method Started with Email: {@Request}", request.Name);
                Entities.Warehouse warehouse = new Entities.Warehouse()
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country, 
                    UserId = userID
                };
                var query = _dbSet.Add(warehouse);
                var result = await _context.SaveChangesAsync();
                if (result > 0 )
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogWarning("Faild to add a duplicate warehouse with Name : {Name}. Unique constraint violation.", request.Name);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while WareHouseRepository/CreateWareHouse : ", request.Name);
                throw ex;
            }

        }
        public async Task<List<WareHouseDto>> GetWareHousesByUserID(int userID)
        {
            try
            {
                _logger.LogInformation("WareHouseRepository/CreateWareHouse method Started for user : ", userID);
                var query = await _dbSet.Where(wh=> wh.UserId == userID).Select(wh => new WareHouseDto
                {
                    Id = wh.Id,
                    Name=wh.Name,
                    Address=wh.Address,
                    City=wh.City,
                    Country=wh.Country,
                })
                   .ToListAsync();

                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while WareHouseRepository/CreateWareHouse for user : ",userID);
                throw ex;
            }

        }

    } 
}
