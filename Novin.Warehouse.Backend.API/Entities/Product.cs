using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities.Base;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Product : Thing
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int MinQuantity { get; set; } = 0;

        public Category? Category { get; set; }
    }
}