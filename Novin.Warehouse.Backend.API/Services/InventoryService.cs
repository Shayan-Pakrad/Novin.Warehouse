using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DTOs.Inventories;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Mappers;

namespace Novin.Warehouse.Backend.API.Services
{
    public class InventoryService : IService<Inventory, InventoryDto, InventoryAddOrUpdateDto>
    {
        private readonly IRepository<Inventory> _inventories;
        private readonly IRepository<Product> _products;

        public InventoryService(IRepository<Inventory> inventories, IRepository<Product> products)
        {
            _inventories = inventories;
            _products = products;
        }

        public async Task<IEnumerable<InventoryDto>> ListAsync()
        {
            return await _inventories.GetAll()
                .Include(m => m.Product)
                .Include(m => m.Product.Category)
                .Select(I => I.ToInventoryDto())
                .ToListAsync();
        }

        public async Task<int> AddAsync(InventoryAddOrUpdateDto entity)
        {
            var productId = (await _products.GetByGuidAsync(entity.ProductGuid))?.Id ?? 0;
            var i = entity.ToInventoryFromInventoryDto(productId);
            return await _inventories.AddAsync(i);
        }

        public async Task<int> RemoveAsync(string guid)
        {
            var dbInventory = await _inventories.GetByGuidAsync(guid);
            if (dbInventory != null)
            {
                return await _inventories.RemoveAsync(dbInventory);
            }
            return 0;
        }

        public async Task<int> UpdateAsync(string guid, InventoryAddOrUpdateDto entity)
        {
            var dbInventory = await _inventories.GetByGuidAsync(guid);
            if (dbInventory != null)
            {
                dbInventory.Location = entity.Location;
                dbInventory.ProductId = (await _products.GetByGuidAsync(entity.ProductGuid))?.Id ?? 0;
                dbInventory.Quantity = entity.Quantity;
                
                return await _inventories.UpdateAsync(dbInventory);
            }
            return 0;
        }
    }
}