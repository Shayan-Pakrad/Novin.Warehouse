using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.DbContexts
{
    public class WarehouseDB : IdentityDbContext<WarehouseUser>
    {
        public WarehouseDB(DbContextOptions options) : base(options)
        {    
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}