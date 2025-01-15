using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Novin.Warehouse.Backend.API.Entities.Base;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.Controllers.Base
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class BaseApiController<TService, TEntity, TEntityDto, TAddUpdateDto> : ControllerBase
        where TService : IService<TEntity, TEntityDto, TAddUpdateDto>
        where TEntity : Thing
    {
        protected readonly TService _service;

        public BaseApiController(TService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public virtual async Task<IResult> ListAsync()
        {
            return Results.Ok(await _service.ListAsync());
        }

        [HttpPost("add")]
        [Authorize(Policy = "RequireAdminRole")]
        public virtual async Task<int> AddAsync(TAddUpdateDto entity)
        {
            return await _service.AddAsync(entity);
        }

        [HttpPut("update/{guid}")]
        public virtual async Task<int> UpdateAsync(TAddUpdateDto entity, string guid)
        {
            return await _service.UpdateAsync(guid, entity);
        }

        [HttpDelete("remove/{guid}")]
        public virtual async Task<int> RemoveAsync(string guid)
        {
            return await _service.RemoveAsync(guid);
        }
    }
}