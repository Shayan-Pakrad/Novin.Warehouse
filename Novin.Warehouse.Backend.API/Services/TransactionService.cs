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

        public async Task<int> AddAsync(TransactionAddOrUpdateDto entity)
        {
            var productId = (await _products.GetByGuidAsync(entity.ProductGuid))?.Id ?? 0;
            var t = entity.ToTransactionFromTransactionDto(productId);
            return await _transactions.AddAsync(t);
        }

        public async Task<int> RemoveAsync(string guid)
        {
            var dbTransaction = await _transactions.GetByGuidAsync(guid);
            if (dbTransaction != null)
            {
                return await _transactions.RemoveAsync(dbTransaction);
            }
            return 0;
        }

        public async Task<int> UpdateAsync(string guid, TransactionAddOrUpdateDto entity)
        {
            var dbTransaction = await _transactions.GetByGuidAsync(guid);
            if (dbTransaction != null)
            {
                dbTransaction.ProductId = (await _products.GetByGuidAsync(entity.ProductGuid))?.Id ?? 0;
                dbTransaction.Quantity = entity.Quantity;
                dbTransaction.Type = entity.Type;

                return await _transactions.UpdateAsync(dbTransaction);
            }
            return 0;
        }
    }
}