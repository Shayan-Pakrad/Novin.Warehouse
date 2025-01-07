using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.Entities.Base;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Thing
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.GetDbSet<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public virtual async Task<TEntity?> GetByGuidAsync(string guid)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Guid == guid);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _unitOfWork.SaveAsync();
        }
    }
}