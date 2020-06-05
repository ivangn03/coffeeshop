using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Resources;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;
using CoffeeShop.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CoffeeShop.API.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICRUDRepoService<User, UserResponse> userService;
        private readonly IMapper mapper;
        public UsersController(ICRUDRepoService<User,UserResponse> userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resource = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var topping = mapper.Map<SaveUserResource, User>(resource);
            var result = await userService.SaveAsync(topping);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var topping = mapper.Map<SaveUserResource, User>(resource);
            topping.Id = id;
            var result = await userService.UpdateAsync(topping);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }
        [Authorize]//(Roles="admin")
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var topping = mapper.Map<DeleteUserResource, User>(resource);
            var result = await userService.DeleteAsync(topping);
            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }
    }
}