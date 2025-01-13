using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Novin.Warehouse.Backend.API.DTOs.Security;
using Novin.Warehouse.Backend.API.Entities;

namespace Novin.Warehouse.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityApiController : ControllerBase
    {
        private readonly IServiceProvider _sp;

        public SecurityApiController(IServiceProvider sp)
        {
            _sp = sp;
        }

        [HttpPost("login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login(LoginRequestDto loginRequest)
        {
            var signInManager = _sp.GetRequiredService<SignInManager<WarehouseUser>>();

 
            var result = await signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);


            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status200OK);
            }

            return TypedResults.Empty;
        }
    }
}