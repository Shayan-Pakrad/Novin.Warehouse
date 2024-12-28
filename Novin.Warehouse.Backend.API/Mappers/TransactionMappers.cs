using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Transactions;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Mappers
{
    public static class TransactionMappers
    {
        public static TransactionDto ToTransactionDto(this Transaction transactionModel)
        {
            return new TransactionDto
            {
                Guid = transactionModel.Guid,
                Quantity = transactionModel.Quantity,
                TransactionDate = transactionModel.TransactionDate,
                TransactionType = transactionModel.Type ? "Recieve" : "Dispatch",
                Product = transactionModel.Product.ToProductDto()
            };
        }

        public static Transaction ToTransactionFromTransactionDto(this TransactionAddOrUpdateDto transactionDto, int productId)
        {
            return new Transaction
            {
                ProductId = productId,
                Quantity = transactionDto.Quantity,
                TransactionDate = transactionDto.TransactionDate,
                Type = transactionDto.Type
            };
        }
    }
}