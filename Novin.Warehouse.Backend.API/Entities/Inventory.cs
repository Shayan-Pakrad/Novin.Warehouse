using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities.Base;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Inventory : Thing
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
        public Product Product { get; set; } = null!;
    }
}