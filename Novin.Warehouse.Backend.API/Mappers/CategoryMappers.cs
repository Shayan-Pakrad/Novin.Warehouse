using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Categories;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Guid = categoryModel.Guid,
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                Products = categoryModel.Products.Select(p => p.Name).ToList()
            };
        }

        public static Category ToCategoryFromCategoryDto(this CategoryAddOrUpdateDto categoryDto)
        {
            return new Category
            {
                Description = categoryDto.Description,
                Name = categoryDto.Name,
            };
        }
    }
}