using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Interfaces;
using Novin.Warehouse.Backend.API.Middlewares;
using Novin.Warehouse.Backend.API.Repositories;
using Novin.Warehouse.Backend.API.Services;
using Novin.Warehouse.Backend.API.UnitOfWorks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your token in the text input below.\nExample: 'abc123'",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.MapIdentityApi<WarehouseUser>();

using (var scope = app.Services.CreateScope())
{
    await SecuritySeed.FirstRun(scope.ServiceProvider);
}

app.MapControllers();

app.Run();