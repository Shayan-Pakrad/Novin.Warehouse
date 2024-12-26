using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.DTOs.Inventories
{
    public class InventoryAddOrUpdateDto
    {
        public required string ProductGuid { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
    }
}