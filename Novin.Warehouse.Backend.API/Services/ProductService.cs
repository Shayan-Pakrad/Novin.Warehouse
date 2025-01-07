using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DTOs.Products;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Mappers;

namespace Novin.Warehouse.Backend.API.Services
{
    public class ProductService : IService<Product, ProductDto, ProductAddOrUpdateDto>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Category> _categories;

        public ProductService(IRepository<Product> products, IRepository<Category> categories)
        {
            _products = products;
            _categories = categories;
        }

        public async Task<IEnumerable<ProductDto>> ListAsync()
        {
            return await _products.GetAll()
                .Include(m => m.Category)
                .Select(p => p.ToProductDto())
                .ToListAsync();
        }

        public async Task<int> AddAsync(ProductAddOrUpdateDto entity)
        {
            if (entity.Price < 0 || entity.MinQuantity < 0) 
            {
                throw new ArgumentException("Product parameters cannot be negative.", nameof(entity)); 
            }

            if (entity.CategoryGuid != null)
            {
                var CategoryId = (await _categories.GetByGuidAsync(entity.CategoryGuid))?.Id ?? 0;
                var p = entity.ToProductFromProductDto(CategoryId);
                return await _products.AddAsync(p);
            }
            else
            {
                var p = entity.ToProductFromProductDto(null);
                return await _products.AddAsync(p);
            }
        }

        public async Task<int> RemoveAsync(string guid)
        {
            var dbProduct = await _products.GetByGuidAsync(guid);
            if (dbProduct != null)
            {
                return await _products.RemoveAsync(dbProduct);
            }
            return 0;
        }

        public async Task<int> UpdateAsync(string guid, ProductAddOrUpdateDto entity)
        {
            if (entity.Price < 0 || entity.MinQuantity < 0) 
            {
                throw new ArgumentException("Product parameters cannot be negative.", nameof(entity)); 
            }
            
            var dbProduct = await _products.GetByGuidAsync(guid);
            if (dbProduct != null)
            {
                if (entity.CategoryGuid != null){
                    dbProduct.CategoryId = (await _categories.GetByGuidAsync(entity.CategoryGuid))?.Id??0;
                }
                dbProduct.Description = entity.Description;
                dbProduct.MinQuantity = entity.MinQuantity;
                dbProduct.Price = entity.Price;
                dbProduct.Name = entity.Name;
                return await _products.UpdateAsync(dbProduct);
            }
            return 0;
        }
    }
}