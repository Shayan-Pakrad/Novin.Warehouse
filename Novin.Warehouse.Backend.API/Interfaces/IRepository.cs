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
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByGuidAsync(string guid);
        Task<int> RemoveAsync(TEntity entity);
        Task<int> SaveAsync();
    }
}