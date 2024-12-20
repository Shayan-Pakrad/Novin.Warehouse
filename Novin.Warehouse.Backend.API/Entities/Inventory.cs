using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
        public Product Product { get; set; } = null!;
    }
}