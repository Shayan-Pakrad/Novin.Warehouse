using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.DTOs.Transactions
{
    public class TransactionAddOrUpdateDto
    {
        public bool Type { get; set; } // true : Receive, false : Dispatch
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public required string ProductGuid { get; set; }
    }
}