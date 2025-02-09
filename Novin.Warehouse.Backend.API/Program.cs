using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Middlewares;
using Novin.Warehouse.Backend.API.Repositories;
using Novin.Warehouse.Backend.API.Services;
using Novin.Warehouse.Backend.API.UnitOfWorks;
using Microsoft.OpenApi.Models;
using Novin.Warehouse.Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

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
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireAuthenticatedUser());
});
builder.Services.AddIdentityApiEndpoints<WarehouseUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<WarehouseDB>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await SecuritySeed.FirstRun(scope.ServiceProvider);
}

app.MapControllers();

app.Run();