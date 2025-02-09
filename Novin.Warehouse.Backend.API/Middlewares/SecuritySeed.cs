using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Novin.Warehouse.Backend.API.DbContexts;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Middlewares
{
    public static class SecuritySeed
    {
        public static async Task FirstRun(IServiceProvider sp)
        {
            var db = sp.GetRequiredService<WarehouseDB>();
            var signInManager = sp.GetRequiredService<SignInManager<WarehouseUser>>();

            db.Database.Migrate();

            var adminRole = await db.Roles.FirstOrDefaultAsync(m => m.Name == "Admin");
            if (adminRole == null)
            {
                var role = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                await db.Roles.AddAsync(role);
                await db.SaveChangesAsync();
                adminRole = role;
            }

            var adminUser = await signInManager.UserManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var admin = new WarehouseUser
                {
                    Email = "admin@local.local",
                    FullName = "مدیریت سیستم",
                    UserName = "admin"
                };
                await signInManager.UserManager.CreateAsync(admin, "admin");
                await db.UserRoles.AddAsync(new IdentityUserRole<string>{
                    RoleId = adminRole.Id,
                    UserId = admin.Id
                });
                await db.SaveChangesAsync();
            }
            
        }
    }
}