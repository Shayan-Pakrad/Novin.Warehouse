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

        public async Task<ProductDto> AddAsync(ProductAddOrUpdateDto entity)
        {

            if (entity.Price < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Price), "Price cannot be negative.");

            if (entity.MinQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.MinQuantity), "Minimum quantity cannot be negative.");

            int? categoryId = null;
            if (!string.IsNullOrWhiteSpace(entity.CategoryGuid))
            {
                var category = await _categories.GetByGuidAsync(entity.CategoryGuid) 
                    ?? throw new InvalidOperationException($"Category with GUID {entity.CategoryGuid} not found.");
                categoryId = category.Id;
            }
            var product = entity.ToProductFromProductDto(categoryId);
            var createdProduct = await _products.AddAsync(product);

            return createdProduct.ToProductDto();
        }

        public async Task<int> RemoveAsync(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            var dbProduct = await _products.GetByGuidAsync(guid);
            if (dbProduct != null)
                return await _products.RemoveAsync(dbProduct);
            
            return 0;
        }

        public async Task<ProductDto> UpdateAsync(string guid, ProductAddOrUpdateDto entity)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Price < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Price), "Price cannot be negative");

            if (entity.MinQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.MinQuantity), "Minimum quantity cannot be null");

            var dbProduct = await _products.GetByGuidAsync(guid)
                ?? throw new InvalidOperationException($"Product with GUID {guid} not found.");

            if (!string.IsNullOrWhiteSpace(entity.CategoryGuid))
            {
                var category = await _categories.GetByGuidAsync(entity.CategoryGuid)
                    ?? throw new InvalidOperationException($"Category with GUID {entity.CategoryGuid} not found.");

                dbProduct.CategoryId = category.Id;
            }

            dbProduct.Name = entity.Name;
            dbProduct.Description = entity.Description;
            dbProduct.MinQuantity = entity.MinQuantity;
            dbProduct.Price = entity.Price;

            var updatedProduct = await _products.UpdateAsync(dbProduct);

            return updatedProduct.ToProductDto();
        }
    }
}