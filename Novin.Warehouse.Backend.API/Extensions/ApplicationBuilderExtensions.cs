using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Novin.Warehouse.Backend.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAngularApp");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}