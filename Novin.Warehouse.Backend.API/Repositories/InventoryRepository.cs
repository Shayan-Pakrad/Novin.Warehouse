using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>
    {
        public InventoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual async Task<Inventory?> GetByProductIdAsync(int productId)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.ProductId == productId);
        }
    }
}