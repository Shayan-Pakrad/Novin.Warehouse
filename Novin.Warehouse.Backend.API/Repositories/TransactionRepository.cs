using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<int> AddAsync(Transaction transaction)
        {
            var inventoryRepository = (InventoryRepository)_unitOfWork.GetRepository<Inventory>();

            var inventory = await inventoryRepository.GetByProductIdAsync(transaction.ProductId) ?? throw new Exception("There is no inventory for this product");
            
            if (transaction.Type == true) // receive
            {
                inventory.Quantity += transaction.Quantity;
            }
            else { // dispatch
                inventory.Quantity -= transaction.Quantity;
            }

            if (inventory.Quantity < 0)
            {
                throw new Exception("Don't have much quantity in inventory");
            }

            await base.AddAsync(transaction);
            return await inventoryRepository.UpdateAsync(inventory);
        }

        public override async Task<int> RemoveAsync(Transaction transaction)
        {
            var inventoryRepository = (InventoryRepository)_unitOfWork.GetRepository<Inventory>();

            var inventory = await inventoryRepository.GetByProductIdAsync(transaction.ProductId) ?? throw new Exception("There is no inventory for this product");

            if (transaction.Type == true) // receive
            {
                inventory.Quantity -= transaction.Quantity;
            }
            else { // dispatch
                inventory.Quantity += transaction.Quantity;
            }

            if (inventory.Quantity < 0)
            {
                throw new Exception("Don't have much quantity in inventory");
            }

            await base.RemoveAsync(transaction);
            return await inventoryRepository.UpdateAsync(inventory);
        }
    }
}