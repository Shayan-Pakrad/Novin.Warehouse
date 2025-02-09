using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Repositories;
using Novin.Warehouse.Backend.API.Services;
using Novin.Warehouse.Backend.API.UnitOfWorks;

namespace Novin.Warehouse.Backend.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseDB>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Warehouse"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IRepository<Transaction>, TransactionRepository>();
            services.AddScoped<IRepository<Inventory>, InventoryRepository>();
            services.AddScoped<ProductService, ProductService>();
            services.AddScoped<CategoryService, CategoryService>();
            services.AddScoped<InventoryService, InventoryService>();
            services.AddScoped<TransactionService, TransactionService>();
        
            return services;
        }
    }
}