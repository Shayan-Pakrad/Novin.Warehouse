using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Products;

namespace Novin.Warehouse.Backend.API.DTOs.Transactions
{
    public class TransactionDto
    {
        public required string Guid { get; set; }
        public required string TransactionType { get; set; } // Receive or Dispatch
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public required ProductDto Product { get; set; }
    }
}