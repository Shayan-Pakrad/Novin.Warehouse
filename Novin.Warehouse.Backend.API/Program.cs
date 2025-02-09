using Novin.Warehouse.Backend.API.Middlewares;
using Novin.Warehouse.Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerWithJwt()
    .AddCustomCors()
    .AddCustomServices(builder.Configuration)
    .AddAuthenticationAndAuthorization()
    .AddIdentityConfiguration()
    .AddControllers();

var app = builder.Build();

app.ConfigureMiddleware();

using (var scope = app.Services.CreateScope())
{
    await SecuritySeed.FirstRun(scope.ServiceProvider);
}

app.MapControllers();

app.Run();