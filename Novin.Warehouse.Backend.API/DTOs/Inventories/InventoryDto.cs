using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Products;

namespace Novin.Warehouse.Backend.API.DTOs.Inventories
{
    public class InventoryDto
    {
        public required string Guid { get; set; }
        public required ProductDto Product { get; set; }
        public int Quantity { get; set; }        
    }
}