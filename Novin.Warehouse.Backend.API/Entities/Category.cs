using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}