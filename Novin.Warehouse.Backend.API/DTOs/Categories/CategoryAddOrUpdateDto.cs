using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.DTOs.Categories
{
    public class CategoryAddOrUpdateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}