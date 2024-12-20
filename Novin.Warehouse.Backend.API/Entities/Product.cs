using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int MinQuantity { get; set; } = 0;

        public Category? Category { get; set; }
    }
}