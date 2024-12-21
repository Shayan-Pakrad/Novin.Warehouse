using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities.Base;
using Novin.Warehouse.Backend.API.Interfaces;

namespace Novin.Warehouse.Backend.API.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WarehouseDB _db;
        private readonly IServiceProvider _sp;

        public UnitOfWork(WarehouseDB db, IServiceProvider sp)
        {
            _db = db;
            _sp = sp;
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : Thing
        {
            return _db.Set<TEntity>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Thing
        {
            var result = _sp.GetServices(typeof(IRepository<TEntity>));
            if (result != null){
                return (IRepository<TEntity>)result;
            }
            throw new Exception("Unknown Service");
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}