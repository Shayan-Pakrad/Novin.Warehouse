using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novin.Warehouse.Backend.API.Controllers.Base;
using Novin.Warehouse.Backend.API.DTOs.Products;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Services;

namespace Novin.Warehouse.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : BaseApiController<ProductService, Product, ProductDto, ProductAddOrUpdateDto>
    {
        public ProductApiController(ProductService service) : base(service)
        {
        }
    }
}