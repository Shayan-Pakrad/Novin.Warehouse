using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DTOs.Transactions;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Mappers;

namespace Novin.Warehouse.Backend.API.Services
{
    public class TransactionService : IService<Transaction, TransactionDto, TransactionAddOrUpdateDto>
    {
        private readonly IRepository<Transaction> _transactions;
        private readonly IRepository<Product> _products;

        public TransactionService(IRepository<Transaction> transactions, IRepository<Product> products)
        {
            _transactions = transactions;
            _products = products;
        }

        public async Task<IEnumerable<TransactionDto>> ListAsync()
        {
            return await _transactions.GetAll()
                .Include(m => m.Product)
                .Select(t => t.ToTransactionDto())
                .ToListAsync();
        }

        public async Task<TransactionDto> AddAsync(TransactionAddOrUpdateDto entity)
        {
            if (entity.Quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "Quantity cannot be negative.");

            var product = await _products.GetByGuidAsync(entity.ProductGuid)
                ?? throw new InvalidOperationException($"Product with GUID {entity.ProductGuid} not found.");

            var transaction = entity.ToTransactionFromTransactionDto(product.Id);
            var createdTransaction = await _transactions.AddAsync(transaction);
            return createdTransaction.ToTransactionDto();          
        }

        public async Task<int> RemoveAsync(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            var dbTransaction = await _transactions.GetByGuidAsync(guid);
            if (dbTransaction != null)
            {
                return await _transactions.RemoveAsync(dbTransaction);
            }
            return 0;
        }

        public async Task<TransactionDto> UpdateAsync(string guid, TransactionAddOrUpdateDto entity)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "Quantity cannot be null.");
            
            var dbTransaction = await _transactions.GetByGuidAsync(guid)
                ?? throw new InvalidOperationException($"Transaction with GUID {guid} not found.");

            
            var product = await _products.GetByGuidAsync(entity.ProductGuid)
                ?? throw new InvalidOperationException($"Product with GUID {guid} not found");
            
            dbTransaction.ProductId = product.Id;
            dbTransaction.Quantity = entity.Quantity;
            dbTransaction.Type = entity.Type;

            var updatedTransaction = await _transactions.UpdateAsync(dbTransaction);

            return updatedTransaction.ToTransactionDto();
        }
    }
}