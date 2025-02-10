using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<Product> AddAsync(Product product)
        {
            var inventoryRepository = _unitOfWork.GetRepository<Inventory>();

            await base.AddAsync(product);
            
            var newInventory = new Inventory
            {
                ProductId = product.Id,
                Quantity = 0,
            };

            await inventoryRepository.AddAsync(newInventory);

            return product;
        }

    }
}