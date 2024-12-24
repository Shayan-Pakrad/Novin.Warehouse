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

        public async Task<int> AddAsync(CategoryAddOrUpdateDto entity)
        {
            var c = entity.ToCategoryFromCategoryDto();
            return await _categories.AddAsync(c);
        }

        public async Task<int> RemoveAsync(string guid)
        {
            var dbCategory = await _categories.GetByGuidAsync(guid);
            if (dbCategory != null)
            {
                return await _categories.RemoveAsync(dbCategory);
            }
            return 0;
        }

        public async Task<int> UpdateAsync(string guid, CategoryAddOrUpdateDto entity)
        {
            var dbCategory = await _categories.GetByGuidAsync(guid);
            if (dbCategory != null)
            {
                dbCategory.Description = entity.Description;
                dbCategory.Name = entity.Name;
                return await _categories.UpdateAsync(dbCategory);
            }
            return 0;
        }
    }
}