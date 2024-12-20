using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.Entities.Base;

namespace Novin.Warehouse.Backend.API.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Thing;
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : Thing;
        Task<int> SaveAsync();
    }
}