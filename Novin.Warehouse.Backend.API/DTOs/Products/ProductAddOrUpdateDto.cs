using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.DTOs.Products
{
    public class ProductAddOrUpdateDto
    {
        public required string Name { get; set; }
        public string? CategoryGuid { get; set; }
        public string? Description { get; set; }
        public required int MinQuantity { get; set; }
    }
}