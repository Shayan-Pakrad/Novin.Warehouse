using Novin.Warehouse.Backend.API.Middlewares;
using Novin.Warehouse.Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerWithJwt();
builder.Services.AddCorsPolicy();
builder.Services.AddCustomServices(builder.Configuration);

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