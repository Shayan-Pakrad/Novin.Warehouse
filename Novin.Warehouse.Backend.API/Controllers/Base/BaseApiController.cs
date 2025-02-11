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
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> ListAsync()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpPost("add")]
        [Authorize(Policy = "RequireAdminRole")]
        public virtual async Task<ActionResult<TEntityDto>> AddAsync(TAddUpdateDto entity)
        {
            var createdEntity = await _service.AddAsync(entity);
            return Created("", createdEntity);
        }

        [HttpPut("update/{guid}")]
        [Authorize(Policy = "RequireAdminRole")]
        public virtual async Task<ActionResult<TEntityDto>> UpdateAsync(TAddUpdateDto entity, string guid)
        {
            var updatedEntity = await _service.UpdateAsync(guid, entity);
            if (updatedEntity == null)
                return NotFound();

            return Ok(updatedEntity);
        }

        [HttpDelete("remove/{guid}")]
        [Authorize(Policy = "RequireAdminRole")]
        public virtual async Task<ActionResult> RemoveAsync(string guid)
        {
            var deletedRows = await _service.RemoveAsync(guid);
            if (deletedRows == 0)
                return NotFound();
            
            return Ok(deletedRows);
        }
    }
}