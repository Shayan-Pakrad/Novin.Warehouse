using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Repositories;
using Novin.Warehouse.Backend.API.Services;
using Novin.Warehouse.Backend.API.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<WarehouseDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Warehouse"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<IRepository<Transaction>, TransactionRepository>();
builder.Services.AddScoped<IRepository<Inventory>, InventoryRepository>();
builder.Services.AddScoped<ProductService, ProductService>();
builder.Services.AddScoped<CategoryService, CategoryService>();
builder.Services.AddScoped<InventoryService, InventoryService>();
builder.Services.AddScoped<TransactionService, TransactionService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();