using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.DTOs.Categories
{
    public class CategoryDto
    {
        public required string Guid { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<string> Products { get; set; } = [];
    }
}