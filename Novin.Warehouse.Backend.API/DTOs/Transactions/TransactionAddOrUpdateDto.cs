using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.DTOs.Transactions
{
    public class TransactionAddOrUpdateDto
    {
        public required string TransactionType { get; set; } // Receive or Dispatch
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public required string ProductGuid { get; set; }
    }
}