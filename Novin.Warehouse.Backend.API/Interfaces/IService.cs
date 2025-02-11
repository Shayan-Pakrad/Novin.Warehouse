using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities.Base;

namespace Novin.Warehouse.Backend.API.Interfaces
{
    public interface IService<TEntity, TEntityDto, TAddUpdateDto> where TEntity : Thing
    {
        Task<IEnumerable<TEntityDto>> ListAsync();
        Task<TEntityDto> AddAsync(TAddUpdateDto entity);
        Task<TEntityDto> UpdateAsync(string guid, TAddUpdateDto entity);
        Task<int> RemoveAsync(string guid);
    }
}