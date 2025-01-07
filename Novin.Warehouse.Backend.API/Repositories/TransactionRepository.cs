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

            await base.AddAsync(transaction);

            var inventory = await inventoryRepository.GetByProductIdAsync(transaction.ProductId);

            if (inventory == null) {
                throw new Exception("There is no inventory for this product");
            }

            if (transaction.Type == true) // receive
            {
                inventory.Quantity += transaction.Quantity;
            }
            else { // dispatch
                inventory.Quantity -= transaction.Quantity;
            }

            return await inventoryRepository.UpdateAsync(inventory);
        }
    }
}