using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Novin.Warehouse.Backend.API.Entities
{
    public class WarehouseUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}