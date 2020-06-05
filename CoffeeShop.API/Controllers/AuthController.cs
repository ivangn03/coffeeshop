using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.API.Extensions;
namespace CoffeeShop.API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController:Controller
    {
       private readonly IMapper mapper;
       private readonly IAuthService authService;
       public AuthController(IMapper mapper, IAuthService authService)
       {
           this.mapper = mapper;
           this.authService = authService;
       }
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginResource user){ 
        if(!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var authResponse = await authService.AuthenticateAsync(user.Login, user.Password);
        var result = mapper.Map<User, UserResource>(authResponse.User);
        return Ok(
            new ResponseData{
                Success = authResponse.Success,
                Message = authResponse.Message,
                Data = result
            }
        );
    }
    
    }
}