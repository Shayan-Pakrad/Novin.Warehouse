using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novin.Warehouse.Backend.API.Controllers.Base;
using Novin.Warehouse.Backend.API.DTOs.Transactions;
using Novin.Warehouse.Backend.API.Entities;
using Novin.Warehouse.Backend.API.Services;

namespace Novin.Warehouse.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionApiController : BaseApiController<TransactionService, Transaction, TransactionDto, TransactionAddOrUpdateDto>
    {
        public TransactionApiController(TransactionService service) : base(service)
        {
        }
    }
}