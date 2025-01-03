using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.DTOs.Products;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Guid = productModel.Guid,
                Name = productModel.Name,
                Price = productModel.Price,
                CategoryName = productModel.Category?.Name??null,
                Description = productModel.Description,
                MinQuantity = productModel.MinQuantity
            };
        }

        public static Product ToProductFromProductDto(this ProductAddOrUpdateDto productDto, int? CategoryId)
        {
            return new Product
            {
                CategoryId = CategoryId,
                Description = productDto.Description,
                MinQuantity = productDto.MinQuantity,
                Name = productDto.Name,
                Price = productDto.Price
            };
        }
    }
}