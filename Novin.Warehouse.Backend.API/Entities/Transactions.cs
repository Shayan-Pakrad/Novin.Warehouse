using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool Type { get; set; } // true : in, false: out
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public Product Product { get; set; } = null!;
    }
}