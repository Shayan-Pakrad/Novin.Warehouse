using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DTOs.Categories;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Mappers;

namespace Novin.Warehouse.Backend.API.Services
{
    public class CategoryService : IService<Category, CategoryDto, CategoryAddOrUpdateDto>
    {
        private readonly IRepository<Category> _categories;

        public CategoryService(IRepository<Category> categories)
        {
            _categories = categories;
        }

        public async Task<IEnumerable<CategoryDto>> ListAsync()
        {
            return await _categories.GetAll()
                .Include(c => c.Products)
                .Select(c => c.ToCategoryDto())
                .ToListAsync();
        }

        public async Task<CategoryDto> AddAsync(CategoryAddOrUpdateDto entity)
        {
            var category = entity.ToCategoryFromCategoryDto();
            var createdCategory = await _categories.AddAsync(category);
            return createdCategory.ToCategoryDto();
        }

        public async Task<int> RemoveAsync(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            var dbCategory = await _categories.GetByGuidAsync(guid);
            if (dbCategory != null)
            {
                return await _categories.RemoveAsync(dbCategory);
            }
            return 0;
        }

        public async Task<CategoryDto> UpdateAsync(string guid, CategoryAddOrUpdateDto entity)
        {
            if (string.IsNullOrWhiteSpace(guid))
                throw new ArgumentException("GUID cannot be null or empty.", nameof(guid));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            var dbCategory = await _categories.GetByGuidAsync(guid)
                ?? throw new InvalidOperationException($"Category with GUID {guid} not found.");

            dbCategory.Name = entity.Name;
            dbCategory.Description = entity.Description;
            
            var updatedCategory = await _categories.UpdateAsync(dbCategory);

            return updatedCategory.ToCategoryDto();
        }
    }
}