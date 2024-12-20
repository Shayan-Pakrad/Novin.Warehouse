using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Warehouse.Backend.API.Entities.Base;

namespace Novin.Warehouse.Backend.API.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Thing
    {
        IQueryable<TEntity> GetAll();
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> RemoveAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByGuidAsync(string guid);
        Task<int> SaveAsync();
    }
}