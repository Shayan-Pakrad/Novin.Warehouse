using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Inventories;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Mappers
{
    public static class InventoryMapper
    {
        public static InventoryDto ToInventoryDto(this Inventory inventoryModel)
        {
            return new InventoryDto
            {
                Guid = inventoryModel.Guid,
                Product = inventoryModel.Product.ToProductDto(),
                Quantity = inventoryModel.Quantity
            };
        }

        public static Inventory ToInventoryFromInventoryDto(this InventoryAddOrUpdateDto inventoryDto, int productId)
        {
            return new Inventory
            {
                ProductId = productId,
                Quantity = inventoryDto.Quantity
            };
        }
    }
}