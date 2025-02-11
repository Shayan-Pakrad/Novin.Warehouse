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

        public async Task<InventoryDto> AddAsync(InventoryAddOrUpdateDto entity)
        {
            if (entity.Quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "Quantity cannot be negative.");

            var product = await _products.GetByGuidAsync(entity.ProductGuid)
                ?? throw new InvalidOperationException($"Product with GUID {entity.ProductGuid} not found");
            
            var inventory = entity.ToInventoryFromInventoryDto(product.Id);
            var createdInventory = await _inventories.AddAsync(inventory);
            return createdInventory.ToInventoryDto();
        }

        public async Task<int> RemoveAsync(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            var dbInventory = await _inventories.GetByGuidAsync(guid);
            if (dbInventory != null)
            {
                return await _inventories.RemoveAsync(dbInventory);
            }
            return 0;
        }

        public async Task<InventoryDto> UpdateAsync(string guid, InventoryAddOrUpdateDto entity)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "Quantity cannot be negative.");

            var dbInventory = await _inventories.GetByGuidAsync(guid)
                ?? throw new InvalidOperationException($"Inventory with GUID {guid} not found.");


            var product = await _products.GetByGuidAsync(entity.ProductGuid)
                ?? throw new InvalidOperationException($"Product with GUID {guid} not found");
            

            dbInventory.ProductId = product.Id;
            dbInventory.Quantity = entity.Quantity;

            var updatedInventory = await _inventories.UpdateAsync(dbInventory);
            return updatedInventory.ToInventoryDto();
        }
    }
}